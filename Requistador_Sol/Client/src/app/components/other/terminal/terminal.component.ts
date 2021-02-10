import { Component, ElementRef, Input, OnInit, Renderer2, ViewChild } from '@angular/core';
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
    
    ngOnInit(): void {
        this.initializeData();
    }

    private initializeData() {}

    onEnter() {
        const badCommandMsg = `Bad command. Type 'help' to get help. It's that easy.`;
        if(!this.terminalInput) {
            this.printToShell('', true);
            this.printToShell(badCommandMsg);
            
            return;
        }

         // joke
         let nix = ['ls', 'pwd', 'pid', 'cd', 'cat', 'touch'];
         const triedNix = nix.some(x => x === this.terminalInput);
         if(triedNix) {
             this.printToShell(this.terminalInput, true);
             this.printToShell('Nice try. This is a dumb demo web app, not a *nix OS');
             
             return;
         }
        
        // TODO: parse command for params
        const command = this.getCommands().find(x => x.name === this.terminalInput);
        if(!command) {
            this.printToShell(this.terminalInput, true);
            this.printToShell(badCommandMsg);
            
            return;
        }

        command.handler();
    }

    private printToShell(message: string, asUserInput: boolean = false) {
        const printLine = (line: string, asUserInput: boolean) => {
            let outputRow = this.renderer.createElement('div');
            
            const text = this.renderer.createText(message);
            const outputColorClass = asUserInput ? 'shell-green' : 'shell-orange';
            
            this.renderer.addClass(outputRow, outputColorClass);
            this.renderer.appendChild(outputRow, text);

            this.renderer.appendChild(this.stdout.nativeElement, outputRow);
        }
        
        asUserInput ? printLine(message, true) : printLine(message, false);
        this.scrollToElement();
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


    private getCommands() {
        return [
            { name: 'help', handler: () => { this.helpHandler() }},
            { name: 'clear', handler: () => { this.clearHandler() } },
            { name: 'app-cfg', handler: () => { this.someHandler() } },
            { name: 'user-cfg', handler: () => { this.someHandler() } },
        ] as ITerminalCommand[];
    }

    
    private helpHandler() {
        let printArray = [ 'Available commands are:'];
        this.getCommands().forEach(x => printArray.push(`[${x.name}]`));
        printArray.push('To obtain more info on command, type:');
        printArray.push('[command] help');

        printArray.forEach(x => this.printToShell(x));
    }

    private clearHandler() {
        const childElements = Array.from(this.stdout.nativeElement.childNodes);
        for (let child of childElements) {
            this.renderer.removeChild(this.stdout.nativeElement, child);
        }
        
        this.clearInput();
    }

    private someHandler() {}

}
