import { ElementRef, OnDestroy, Renderer2 } from "@angular/core";
import { Subject } from "rxjs";
import { ShellService } from "../../shell.service";
import { eShellColor } from "../enums";
import { IShellScript } from "../interfaces/IShellScript";

export abstract class ShellScriptBase implements OnDestroy {
    scriptName: string;
    protected stdout: ElementRef<HTMLDivElement>;
    protected ngUnsubscribe: Subject<any>;
    
    private colorMap: { [key: number]: string }

    constructor(
        scriptName: string,
        stdout: ElementRef<HTMLDivElement>,
        protected renderer: Renderer2,
        protected shellService: ShellService,
        registerInShell: boolean = true
    ) {
        this.scriptName = scriptName;
        this.stdout = stdout;
        
        this.colorMap = {};
        this.colorMap[eShellColor.Regular] = 'shell-regular';
        this.colorMap[eShellColor.Warning] = 'shell-warning';
        this.colorMap[eShellColor.Error] = 'shell-error';
        this.colorMap[eShellColor.User] = 'shell-user';
        
        if(registerInShell) {
            this.ngUnsubscribe = new Subject();
            this.registerInShell();
        }
    }

    protected print(message: string, color: eShellColor = eShellColor.Regular, fingerbang = false) {
        let outputRow = this.renderer.createElement('div');
            
        const text = this.renderer.createText(message);
        const outputColorClass = this.colorMap[color];
        
        this.renderer.addClass(outputRow, outputColorClass);
        this.renderer.appendChild(outputRow, text);
        
        if(fingerbang) {
            let span = this.renderer.createElement('span');
            span.innerHTML = '&#128405;';
            this.renderer.appendChild(outputRow, span);
        }

        this.renderer.appendChild(this.stdout.nativeElement, outputRow);
    }
    abstract execute(option?: string, args?: string[]);
    protected abstract validate(option: string, args: string[]): boolean;
    protected abstract getDocumentation(): string[];


    protected scrollToElement(): void {
        document
            .querySelector('#terminalBottom')
            .scrollIntoView({behavior: "smooth", block: "start", inline: "nearest"});
    }


    private registerInShell() {
        const script = {
            name: this.scriptName,
            manual: this.getDocumentation()
        } as IShellScript;

        this.shellService.invRegisterScript(script);
    }

    
    ngOnDestroy() {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }
}