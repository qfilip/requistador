import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnDestroy, OnInit, Output, Renderer2, ViewChild } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ShellService } from 'src/app/modules/shell';
import { BareShell } from 'src/app/modules/shell/models/classes/bare.shellscript';
import { ShellScriptBase } from 'src/app/modules/shell/models/classes/base.shellscript';
import { ClearScript } from 'src/app/modules/shell/models/classes/clear.shellscript';
import { HelpScript } from 'src/app/modules/shell/models/classes/help.shellscript';
import { ManScript } from 'src/app/modules/shell/models/classes/man.shellscript';
import { eShellColor } from 'src/app/modules/shell/models/enums';
import { PageLoaderService } from 'src/app/services/page-loader.service';
import { IApiAdminRequestDto } from 'src/app/_generated/interfaces';
import { AppcfgScript } from '../../../modules/shell/models/classes/appcfg.shellscript';

@Component({
    selector: 'terminal',
    templateUrl: './terminal.component.html',
    styleUrls: ['./terminal.component.scss']
})
export class TerminalComponent implements OnInit, AfterViewInit, OnDestroy {
    @ViewChild('stdout') stdout: ElementRef<HTMLDivElement>;
    @ViewChild('shellInput') shellInput: ElementRef<HTMLInputElement>;
    @ViewChild('terminalBottom') terminalBottom: ElementRef<HTMLDivElement>;
    
    @Input() maxHeightStyle: string;
    @Output() onShellCloseRequested = new EventEmitter<boolean>();

    constructor(
        private pageLoaderService: PageLoaderService,
        private renderer: Renderer2,
        private shellService: ShellService
    ) { }

    private ngUnsubscribe = new Subject();

    terminalInput: string;
    shell: BareShell;
    shellScripts: ShellScriptBase[];
    
    private history: string[] = [];
    private historyIdx: number;
    
    ngOnInit(): void {
        this.pageLoaderService.show();
        this.manageSubscriptions();
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

        this.shellInput.nativeElement.focus();
        this.pageLoaderService.hide();
    }

    onEnter() {
        this.parseInput();
        this.shell.scrollToBottom();
    }

    onUpkey() {
        if(this.historyIdx > 0) {
            this.historyIdx--;
        }
        else if(this.historyIdx === 0) {
            this.historyIdx = this.history.length - 1;
        }

        this.terminalInput = this.history[this.historyIdx];
    }

    onDownkey() {
        this.historyIdx++;
        if(this.historyIdx ===  this.history.length) {
            this.historyIdx = 0;
        }

        this.terminalInput = this.history[this.historyIdx];
    }


    private parseInput() {
        if(!this.terminalInput && this.terminalInput.length === 0) {
            this.printBadCommand();
            
            return;
        }

        this.historyIdx = this.history.push(this.terminalInput) - 1;
        let [ script, arg ] = this.terminalInput.split('--');
        let [ command, option ] = script.split(' ');
        let args = !!arg ? arg.split(' ') : [];

        // joke
        let nix = ['ls', 'tar', 'pwd', 'pid', 'cd', 'cat', 'touch'];
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

        shellCommand.execute(option, args);
    }


    private printBadCommand(message: string[] = null) {
        const lines = message ?? [`Bad command. Type 'help' to get help. It's that easy.`];
        this.shell.printToShell('', eShellColor.Error);
        lines.forEach(x => this.shell.printToShell(x));
    }


    emitCloseRequest() {
        this.terminalInput = '';
        this.onShellCloseRequested.emit(true);
    }


    private handleAdminRequest(dto: IApiAdminRequestDto) {

    }


    private manageSubscriptions() {
        this.shellService.onAdminRequest
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(x => {
            if(!!x) {
                this.handleAdminRequest(x);
            }
        });
    }

    
    ngOnDestroy() {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }
}
