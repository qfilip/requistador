export interface ITerminalCommand {
    name: string;
    handler: () => void;
}