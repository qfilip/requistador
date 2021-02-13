export class FileFunctions {
    static byte2text(bytes: any, callbackFn: (text: string | ArrayBuffer) => void) {
        // bytes: c# file.txt that is sent

        // convert to array buffer
        const base64ToArrayBuffer = (base64) => {
            let binaryString = window.atob(base64);
            let binaryLen = binaryString.length;
            let bytes = new Uint8Array(binaryLen);
            for (let i = 0; i < binaryLen; i++) {
                let ascii = binaryString.charCodeAt(i);
                bytes[i] = ascii;
            }
            return bytes;
        }

        const base64 = base64ToArrayBuffer(bytes);
        const blob = new Blob([base64], { type: 'text/plain' });
        const reader = new FileReader();
        reader.readAsText(blob);
        reader.onloadend = () => {
            // console.log(reader.result)
            callbackFn(reader.result);
        }
    }
}