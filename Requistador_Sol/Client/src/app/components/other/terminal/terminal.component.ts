import { AfterViewInit, Component, ElementRef, Input, OnInit, Renderer2, ViewChild } from '@angular/core';
import { ShellService } from 'src/app/modules/shell';
import { BareShell } from 'src/app/modules/shell/models/classes/bare.shellscript';
import { ShellScriptBase } from 'src/app/modules/shell/models/classes/base.shellscript';
import { ClearScript } from 'src/app/modules/shell/models/classes/clear.shellscript';
import { HelpScript } from 'src/app/modules/shell/models/classes/help.shellscript';
import { ManScript } from 'src/app/modules/shell/models/classes/man.shellscript';
import { eShellColor } from 'src/app/modules/shell/models/enums';
import { PageLoaderService } from 'src/app/services/page-loader.service';
import { AppcfgScript } from '../../../modules/shell/models/classes/appcfg.shellscript';

@Component({
    selector: 'terminal',
    templateUrl: './terminal.component.html',
    styleUrls: ['./terminal.component.scss']
})
export class TerminalComponent implements OnInit, AfterViewInit {
    @ViewChild('stdout') stdout: ElementRef<HTMLDivElement>;
    @ViewChild('terminalBottom') terminalBottom: ElementRef<HTMLDivElement>;
    
    @Input() maxHeightStyle: string;

    constructor(
        private pageLoaderService: PageLoaderService,
        private renderer: Renderer2,
        private shellService: ShellService
    ) { }

    terminalInput: string;
    shell: BareShell;
    shellScripts: ShellScriptBase[];
    
    ngOnInit(): void {
        this.pageLoaderService.show();
    }

    ngAfterViewInit() {
        const clear = () => { this.terminalInput = '' };
        this.shell = new BareShell(this.stdout, this.renderer, this.shellService, clear);
        
        this.shellScripts = [
            new AppcfgScript(this.stdout, this.renderer, this.shellService),
            new HelpScript(this.stdout, this.renderer, this.shellService),
            new ManScript(this.stdout, this.renderer, this.shellService),
            new ClearScript(this.stdout, this.renderer, this.shellService, clear),
        ];

        this.pageLoaderService.hide();
    }

    onEnter() {
        this.parseInput();
        this.shell.scrollToElement();
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
             this.shell.printToShell(this.terminalInput, eShellColor.User);
             this.shell.printToShell(message, eShellColor.Regular, true);

             return;
        }

        const shellCommand = this.shellScripts.find(x => x.scriptName === command);
        this.shell.printToShell(this.terminalInput, eShellColor.User);
        if(!shellCommand) {
            this.printBadCommand();
            
            return;
        }

        shellCommand.execute(option, arg);
    }


    private printBadCommand(message: string[] = null) {
        const lines = message ?? [`Bad command. Type 'help' to get help. It's that easy.`];
        this.shell.printToShell('', eShellColor.Error);
        lines.forEach(x => this.shell.printToShell(x));
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

        // const manPage = this.shellDocs.getManual(option);
        // manPage.forEach(x => this.printToShell(x));
    }
}
