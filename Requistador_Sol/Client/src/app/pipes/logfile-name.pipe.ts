import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name: 'logfileName'
})
export class LogfileNamePipe implements PipeTransform {
    transform(value: string) {
        const [ year, month, day, hour, minute, second, name ] = value.split('_');
        const result = `${day}-${month}-${year}|${hour}:${minute}:${second}|${name}`;
        
        return result;
    }

}