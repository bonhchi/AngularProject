import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {
  ModalDismissReasons,
  NgbModal,
  NgbModalRef,
} from '@ng-bootstrap/ng-bootstrap';
import {
  FileDtoModel,
  PageModel,
  ReturnMessage,
  SearchPaganationDTO,
} from 'src/app/lib/data/models';
import { FileService } from 'src/app/lib/data/services';
import { CreateImageComponent } from '../create-image/create-image.component';

@Component({
  selector: 'app-list-files',
  templateUrl: './list-files.component.html',
  styleUrls: ['./list-files.component.scss'],
})
export class ListFilesComponent implements OnInit {
  media: FileDtoModel[];
  params: any;
  closeResult: string;
  type: any;

  constructor(
    private fileService: FileService,
    private modalService: NgbModal
  ) {
    this.params = { pageIndex: 0 };
    this.getFiles();
    this.getType();
  }

  public settings = {
    hideSubHeader: true,
    mode: 'external',
    actions: {
      position: 'right',
      add: false,
    },
    columns: {
      id: {
        title: 'ID',
      },
      name: {
        title: 'Name',
      },
      url: {
        title: 'URL',
        type: 'html',
        valuePrepareFunction: (file) => {
          var fileExt = file.split('.').pop();
          if (
            fileExt == 'png' ||
            fileExt == 'jpg' ||
            fileExt == 'jpeg' ||
            fileExt == 'icon'
          ) {
            return `<a href="${FileService.getLinkFile(file)}"><img appUiImageLoader width="75px" height="75px" src="${FileService.getLinkFile(file)}"/></a>`;
          }
          return `<a href="${FileService.getLinkFile(file)}">${FileService.getLinkFile(file)}</a>`;
        },
      },
      fileExt: {
        title: 'FileExt',
      },
      entityType: {
        title: 'EnityType',
      },
      entityId: {
        title: 'EntityID',
      },
    },
  };

  ngOnInit() {}

  async getFiles() {
    await this.fileService
      .getFile({ params: this.params })
      .then(
        (res: ReturnMessage<PageModel<FileDtoModel>>) =>
          (this.media = res.data.results)
      );
    // .catch((er) =>
    // console.log(er.error)
    // );
  }

  async getType() {
    await this.fileService.getType().then((res: ReturnMessage<any>) => {
      // console.log(res.data);
      this.type = res.data;
    });
  }

  open(item: FileDtoModel | null, typeMulti: number) {
    // console.log(item);
    var modalRef: NgbModalRef = this.modalService.open(CreateImageComponent, {
      ariaLabelledBy: 'modal-basic-title',
    });

    modalRef.componentInstance.select = this.type;
    modalRef.componentInstance.typeMulti = typeMulti;
    modalRef.result.then(
      (result) => {
        this.getFiles();
      },
      (reason) => {
        this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
      }
    );
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
}
