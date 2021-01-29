import { eDialogAnimation } from "../enums/eDialogAnimation";

export interface IDialogOptions {
    header: string,
    acceptFn: () => void;
    cancelVisible: boolean;
    okLabel: string;
    cancelLabel: string;
    dialogAnimation: eDialogAnimation
}