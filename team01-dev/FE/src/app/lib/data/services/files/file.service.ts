import { Injectable } from '@angular/core';
import { AppConfig } from 'src/app/lib/environments/config/appConfig';
import { HttpClientService } from 'src/app/lib/http/http-client';

@Injectable()
export class FileService {
  private url = '/api/file';
  private urlDownload = this.url + '/download';
  private urlGetType = this.url + '/type';

  constructor(private httpClient: HttpClientService) {}

  getFile(request: any) {
    return this.httpClient.getObservable(this.url, request).toPromise();
  }

  getType() {
    return this.httpClient.getObservable(this.urlGetType).toPromise();
  }

  saveFile(model: FormData) {
    return this.httpClient.postObservable(this.url, model).toPromise();
  }

  downloadFile(request: any) {
    return this.httpClient.getObservable(this.urlDownload, request).toPromise();
  }

  getLinkDownloadFile(url: string) {
    return `${AppConfig.settings.API_URL}${this.urlDownload}?url=${url}`;
  }

  public static getLinkFile(fileName: String) {
    if (fileName.trim()) {
      var result = fileName.includes('http')
        ? fileName
        : `${AppConfig.settings.API_URL}/Files/${fileName}`;
      return result.toString();
    }
    return '';
  }
}
