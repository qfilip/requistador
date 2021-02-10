export class ShellDocumentation {
    private lookup: Map<string, () => string[]>;
    
    constructor() {
        this.lookup = new Map<string, () => string[]>();
        this.lookup.set('help', this.helpManPage);
        this.lookup.set('clear', this.clearManPage);
        this.lookup.set('appcfg', this.appCfgManPage);
        this.lookup.set('man', this.manManPage);
    }


    getManual(command: string) {
        return this.lookup.get(command)();
    }


    private helpManPage() {
        return [
            '[help]',
            '## Prints out available commands',
            '## No additional options available'
        ];
    }


    private clearManPage() {
        return [
            '[clear]',
            '## Clears previous outputs from the console window',
            '## No additional options available'
        ];
    }


    private appCfgManPage() {
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


    private manManPage() {
        return [
            '[man]',
            '## If you came this far, you know how to use it'
        ];
    }

    // { name: 'help', handler: (opt: string, arg: string) => this.helpHandler() },
    // { name: 'clear', handler: (opt: string, arg: string) => this.clearHandler() },
    // { name: 'app-cfg', handler: (opt: string, arg: string) => this.someHandler(opt, arg) },
    // { name: 'user-cfg', handler: (opt: string, arg: string) => this.someHandler(opt, arg) },
    // { name: 'man', 
}