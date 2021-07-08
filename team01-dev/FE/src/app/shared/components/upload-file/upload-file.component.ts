import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';
import { Observable, Observer } from 'rxjs';
import {
  ReturnMessage,
  FileDtoModel,
  TypeSweetAlertIcon,
} from 'src/app/lib/data/models';
import { FileService } from 'src/app/lib/data/services';
import { MessageService } from 'src/app/lib/data/services/messages/message.service';
import { ModalFile, TypeFile } from '../modals/models/modal.model';

@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.scss'],
})
export class UploadFileComponent implements OnInit {
  @Input() data: ModalFile;
  @Input() styleFile: string;
  @Input() fileURL: (string | ArrayBuffer)[];
  @Input() isBannerUpload = false;
  @Input() sizeImage: string;
  @Output() onChange = new EventEmitter();

  public files: File[];

  public typeIMAGE = TypeFile.IMAGE;

  constructor(
    private fileService: FileService,
    private messageService: MessageService
  ) {
    this.onRemoveLocal();
    this.styleFile = 'width: 450px; height: 200px;';
  }
  ngOnChanges(changes: SimpleChanges): void {
    if (this.fileURL) {
      this.files = [];
      this.fileURL.forEach((res) => {
        if (res) {
          this.convertUrlToFile(FileService.getLinkFile(res.toString())).then(
            (res) => {
              this.files.push(res);
            }
          );
        }
      });
    }
  }

  ngOnInit() {
    if (this.data) {
      this.data.listFile = [];
    }
  }

  // onUpdate()
  // {
  //   this.createImage(this.file);
  // }

  onRemoveLocal() {
    this.fileURL = [];
    this.files = [];
  }

  // onRemoveUpdate(id: string) {
  //   this.data.listFile.forEach((e, i) => {
  //     if (e.id == id) {
  //       this.data.listFile.splice(i, 1);
  //     }
  //   });
  // }

  onDownload(file: FileDtoModel) {
    return this.fileService.getLinkDownloadFile(file.url);
  }

  // onChangeImg(event) {
  //   if (!this.data.multiBoolen) {
  //     this.onRemoveLocal();
  //   }
  //   var files = event.target.files;
  //   Array.from(files).forEach((file: File) => {
  //     if (file.size === 0) {
  //       return;
  //     }
  //     var mimeType = file.type;
  //     if (mimeType.match(/image\/*/) == null) {
  //       alert('Only images are supported.');
  //       return;
  //     }
  //     var reader = new FileReader();
  //     reader.readAsDataURL(file);
  //     reader.onload = (_event) => {
  //       this.fileURL.push(reader.result);
  //     };
  //     this.files.push(file);
  //     // console.log(file);
  //   });
  //   this.createImage(this.files);
  // }

  async createImage(files: File[]) {
    if (files.length <= 0) {
      return;
    }
    var formData = new FormData();
    files.forEach((file) => {
      formData.append('files', file);
    });
    formData.append('entityType', this.data.entityType);
    formData.append('entityId', this.data.entityId);
    await this.fileService
      .saveFile(formData)
      .then((res: ReturnMessage<FileDtoModel[]>) => {
        this.messageService.notification(
          'Tải thành công',
          TypeSweetAlertIcon.SUCCESS
        );
        this.data.listFile = [...this.data.listFile, ...res.data];
        this.actionChange(
          res.data.map((res) => res.url),
          null
        );
      })
      .catch((er) =>
        this.messageService.alert(
          'Tải thất bại',
          TypeSweetAlertIcon.ERROR,
          er.error.message ?? er.error
        )
      );
  }

  actionChange(add: string[], remove: string, removeAll: boolean = false) {
    this.onChange.emit({ add: add, remove: remove, removeAll: removeAll });
  }

  // getImage(imageUrl: string) {
  //   return FileService.getLinkFile(imageUrl);
  // }

  onSelect(event) {
    if (this.isBannerUpload) {
      var reader = new FileReader();
      reader.readAsDataURL(event.addedFiles[0]);
      reader.onload = () => {
        const img = new Image();
        img.src = reader.result as string;
        img.onload = () => {
          const height = img.naturalHeight;
          const width = img.naturalWidth;

          if (height < 500 || width < 250)
            return this.messageService.alert('Size image must over 500x250');
          this.exceptUpload(event);
        };
      };
      return;
    }

    this.exceptUpload(event);
  }

  exceptUpload(event) {
    if (!this.data.multiBoolean) {
      this.onRemoveLocal();
      this.actionChange(null, null, true);
    }
    this.createImage(event.addedFiles);
  }

  onRemove(event) {
    // console.log(event);
    this.files.splice(this.files.indexOf(event), 1);
    this.actionChange(null, event.name);
  }

  convertUrlToFile(url: string) {
    return fetch(url)
      .then((e) => {
        return e.blob();
      })
      .then((blob) => {
        let b: any = blob;
        b.lastModifiedDate = new Date();
        b.name = url.split(/[\\\/]/).pop();
        return b as File;
      });
  }
}
