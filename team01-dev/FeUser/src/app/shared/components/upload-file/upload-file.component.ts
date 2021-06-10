import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  SimpleChanges,
} from "@angular/core";
import { Observable, Observer } from "rxjs";
import { ReturnMessage } from "src/app/lib/data/models";
import { FileDtoModel } from "src/app/lib/data/models/files/file.model";
import { FileService } from "src/app/lib/data/services";
import { ModalFile, TypeFile } from "../../modals/models/modal.model";

@Component({
  selector: "app-upload-file",
  templateUrl: "./upload-file.component.html",
  styleUrls: ["./upload-file.component.scss"],
  providers: [FileService],
})
export class UploadFileComponent implements OnInit {
  @Input() data: ModalFile;
  @Input() styleFile: string;
  @Input() fileURL: (string | ArrayBuffer)[];
  @Output() onChange = new EventEmitter();

  public files: File[];

  public typeIMAGE = TypeFile.IMAGE;

  constructor(private fileService: FileService) {
    this.onRemoveLocal();
    this.styleFile = "width: 450px; height: 200px;";
  }
  ngOnChanges(changes: SimpleChanges): void {
    if (this.fileURL) {
      this.files = [];
      this.fileURL.forEach((res) => {
        if (res == null) return;
        this.converUrltoFile(FileService.getLinkFile(res.toString())).then(
          (res) => {
            this.files.push(res);
          }
        );
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
      formData.append("files", file);
    });
    formData.append("entityType", this.data.enityType);
    formData.append("entityId", this.data.enityId);
    await this.fileService
      .saveFile(formData)
      .then((res: ReturnMessage<FileDtoModel[]>) => {
        this.data.listFile = [...this.data.listFile, ...res.data];
        // this.data.listFile.forEach((it) => {
        //   this.converUrltoFile(it.url).then((file) => this.files.push(file));
        // });
        this.actionChange(
          res.data.map((res) => res.url),
          null
        );
      })
      .catch((er) => {
        // console.log(er.error);
      });
  }

  actionChange(add: string[], remove: string, removeAll: boolean = false) {
    this.onChange.emit({ add: add, remove: remove, removeAll: removeAll });
  }

  getImage(imageUrl: string) {
    return FileService.getLinkFile(imageUrl);
  }

  onSelect(event) {
    if (!this.data.multiBoolen) {
      this.onRemoveLocal();
      this.actionChange(null, null, true);
    }
    this.createImage(event.addedFiles);
    // console.log(this.files);
  }

  onRemove(event) {
    // console.log(event);
    this.files.splice(this.files.indexOf(event), 1);
    this.actionChange(null, event.name);
  }

  converUrltoFile(url: string) {
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
