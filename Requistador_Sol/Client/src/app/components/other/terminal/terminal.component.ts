import { ThrowStmt } from '@angular/compiler';
import { Component, ElementRef, Input, OnInit, Renderer2, ViewChild } from '@angular/core';
import { ShellDocumentation } from '../terminal-utils/shell.documentation';
import { ITerminalCommand } from './terminal.models';

@Component({
    selector: 'terminal',
    templateUrl: './terminal.component.html',
    styleUrls: ['./terminal.component.scss']
})
export class TerminalComponent implements OnInit {
    @ViewChild('stdout') stdout: ElementRef<HTMLDivElement>;
    @ViewChild('terminalBottom') terminalBottom: ElementRef<HTMLDivElement>;
    
    @Input() maxHeightStyle: string;

    constructor(private renderer: Renderer2) { }

    terminalInput: string;
    shellDocs: ShellDocumentation;
    
    ngOnInit(): void {
        this.initializeData();
    }

    private initializeData() {
        this.shellDocs = new ShellDocumentation();
    }

    onEnter() {
        this.parseInput();
        this.scrollToElement();
    }


    private parseInput() {
        if(!this.terminalInput) {
            this.printBadCommand();
            
            return;
        }
        let [command, option, arg] = this.terminalInput
            .split(' ')
            .filter(x => x !== '');

        // joke
        let nix = ['ls', 'pwd', 'pid', 'cd', 'cat', 'touch'];
        const triedNix = nix.some(x => x === command);
        if(triedNix) {
             const message = 'Nice try. This is a dumb demo web app, not a *nix shell ';
             this.printToShell(this.terminalInput, true);
             this.printToShell(message, false, true);

             return;
        }
        
        const shellCommand = this.getCommands().find(x => x.name === command);
        this.printToShell(this.terminalInput, true);
        if(!shellCommand) {
            this.printBadCommand();
            
            return;
        }

        shellCommand.handler(option, arg);
    }


    private printBadCommand(message: string[] = null) {
        const lines = message ?? [`Bad command. Type 'help' to get help. It's that easy.`];
        this.printToShell('', true);
        lines.forEach(x => this.printToShell(x));
    }


    private printToShell(message: string, asUserInput: boolean = false, fingerbang = false) {
        
        const printLine = () => {
            let outputRow = this.renderer.createElement('div');
            
            const text = this.renderer.createText(message);
            const outputColorClass = asUserInput ? 'shell-green' : 'shell-orange';
            
            this.renderer.addClass(outputRow, outputColorClass);
            this.renderer.appendChild(outputRow, text);
            
            if(fingerbang) {
                let span = this.renderer.createElement('span');
                span.innerHTML = '&#128405;';
                this.renderer.appendChild(outputRow, span);
            }

            this.renderer.appendChild(this.stdout.nativeElement, outputRow);
        }
        
        printLine();
        this.clearInput();
        
    }


    private clearInput() {
        this.terminalInput = '';
    }


    private scrollToElement(): void {
        document
            .querySelector('#terminalBottom')
            .scrollIntoView({behavior: "smooth", block: "start", inline: "nearest"});
    }


    private getCommands(opt: string = null, arg: string = null) {
        return [
            { name: 'help', handler: (opt: string, arg: string) => this.helpHandler() },
            { name: 'clear', handler: (opt: string, arg: string) => this.clearHandler() },
            { name: 'appcfg', handler: (opt: string, arg: string) => this.someHandler(opt, arg) },
            { name: 'man', handler: (opt: string, arg: string) => this.manHandler(opt) },
        ] as ITerminalCommand[];
    }

    
    // command handlers
    private helpHandler() {
        let printArray = [ 'Available commands are:'];
        this.getCommands().forEach(x => printArray.push(`[${x.name}]`));
        printArray.push('To obtain more info on command, type:');
        printArray.push('man [command]');

        printArray.forEach(x => this.printToShell(x));
    }

    private clearHandler() {
        const childElements = Array.from(this.stdout.nativeElement.childNodes);
        for (let child of childElements) {
            this.renderer.removeChild(this.stdout.nativeElement, child);
        }
        
        this.clearInput();
    }


    private manHandler(option: string) {
        if(!option || option.length === 0) {
            const msg = [
                'Insufficient arguments for [man] command',
                'Try [man] [command] to get command details',
                'Try [help] to see available commands'
            ];
            
            this.printBadCommand(msg);
            return;
        }

        const manPage = this.shellDocs.getManual(option);
        manPage.forEach(x => this.printToShell(x));
    }

    private someHandler(option: string, param: string) {
        console.log(option);
        console.log(param);
    }

}
