import { ElementRef, Renderer2 } from "@angular/core";
import { eAdminRequestFor } from "src/app/_generated/enums";
import { IApiAdminRequest, IAppStateDto } from "src/app/_generated/interfaces";
import { ShellService } from "../../shell.service";
import { eShellColor } from "../enums";
import { ShellScriptBase } from "./base.shellscript";

export class AppcfgScript extends ShellScriptBase {
    
    private options: string[] = ['timeout'];

    constructor(
        stdout: ElementRef<HTMLDivElement>,
        renderer: Renderer2,
        shellService: ShellService) {
        super('appcfg', stdout, renderer, shellService);
    }

    execute(option: string, args: string[]) {
        const valid = this.validate(option, args);
        if(!valid) {
            return;
        }
        
        const resolvers = this.getCommandResolvers();
        const requestMaker = resolvers[option];
        const request = requestMaker(option, args);

        this.shellService.invokeAdminRequest(request);
    }


    protected validate(option: string, args: string[]) {
        let errors = [];
        const validOptions = this.getOptionsAndArgs();
        const validOptionStrings = Object.keys(validOptions);

        const validString = (s: string) => !s || s.length > 0;
        
        if(!validString(option)) {
            errors.push('Invalid command option specified');
        }
        else if(!validOptionStrings.includes(option)) {
            errors.push(`'${option}' is not valid [appcfg] command option`);
        }
        else if(!args || args.length === 0) {
            errors.push('No argument specified');
        }
        else {
            const argsValidator = validOptions[option];
            args.forEach(x => {
                if(!argsValidator(x)) {
                    errors.push(`'${x}' is not a valid argument for option: '${option}'`);
                }
            });
        }

        
        const valid = errors.length === 0;
        
        if(!valid) {
            errors.push('Try: man appcfg, to get usage info');
            errors.forEach(x => this.print(x, eShellColor.Error));
        }

        return valid;
    }

    
    protected getDocumentation(): string[] {
        return [
            '[appcfg]',
            '## Configures web api settings for this app',
            '',
            'OPTIONS:',
            '[timeout] <int> - sets timeout between request proccesing (in seconds)',
            '   ',
            'EXAMPLE:',
            'appcfg timeout --240',
            '## sets time interval for pending requests processing',
            '## to 240 seconds (4 minutes)'
        ];
    }


    private getOptionsAndArgs() {
        let options: { [opt: string]: (arg: string) => boolean }[] = [];
        const opts = this.options;

        options[opts[0]] = (arg: string) => {
            let valid = !isNaN(parseInt(arg));
            if(valid) {
                valid = valid && (parseInt(arg)) > 1;
            }

            return valid;
        }

        return options;
    }


    private getCommandResolvers() {
        let resolvers: { [opt: string]: (opt: string, args: string[]) => IApiAdminRequest } = {};
        const opts = this.options;
        
        resolvers[opts[0]] = (opt: string, args: string[]) => {
            return { 
                requestFor: eAdminRequestFor.Timeout,
                args: args
            } as IApiAdminRequest;
        }

        return resolvers;
    }
}