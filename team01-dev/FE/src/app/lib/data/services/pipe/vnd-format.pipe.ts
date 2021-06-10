import { Pipe, PipeTransform } from "@angular/core";

@Pipe({ name: 'vndFormat' })
export class VndFormatPipe implements PipeTransform {
    transform(value: number) {
        return value === null || value === undefined || !this.isNumeric(value) ? '' : this.numberWithCommas(value.toLocaleString('vi-VN', {
            style: 'currency',
            currency: 'VND'
        }))
    }
    isNumeric(n) {
        return !isNaN(parseFloat(n)) && isFinite ( n );
    }
    numberWithCommas(n) {
        let val = n;
        const correctFormat = val.toString().replace(/\./g, ',')
        return correctFormat;
    }
}