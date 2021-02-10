export interface ITerminalCommand {
    name: string;
    handler: (option: string, arg: string) => void;
    manual: string[];
}