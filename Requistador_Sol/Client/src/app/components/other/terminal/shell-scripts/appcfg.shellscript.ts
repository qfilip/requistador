import { ElementRef, Renderer2 } from "@angular/core";
import { eShellColor } from "../terminal.models";
import { ShellScriptBase } from "./base.shellscript";

export class AppcfgScript extends ShellScriptBase {
    
    constructor(stdout: ElementRef<HTMLDivElement>, renderer: Renderer2) {
        super('appcfg', stdout, renderer);
    }

    execute(option: string, arg: string) {
        const valid = this.validate(option, arg);
        if(!valid) {
            return;
        }
        
        // TODO: implement handling
    }


    protected validate(option: string, arg: string) {
        let errors = [];
        const validOptions = ['timeout'];

        const validString = (s: string) => !s || s.length > 0;
        
        if(!validString(option)) {
            errors.push('Invalid command option specified');
        }
        else if(!validString(arg)) {
            errors.push('Invalid command argument specified');
        }
        else if(!validOptions.includes(option)) {
            errors.push(`'${option}' is not valid [appcfg] command option`);
        }
        else if(isNaN(parseInt(arg))) {
            errors.push(`'${arg}' is not valid option argument`);
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
            'appcfg timeout 240',
            '## sets time interval for pending requests processing',
            '## to 240 seconds (4 minutes)'
        ];
    }
}