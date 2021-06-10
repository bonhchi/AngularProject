import {
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
} from "@angular/core";
import { CommentModel } from "src/app/lib/data/models/comments/comment.model";
import { PageModel } from "src/app/lib/data/models/common/page.model";
import { TypeDisplayImage } from "src/app/shared/data";

@Component({
  selector: "app-comment-detail",
  templateUrl: "./comment-detail.component.html",
  styleUrls: ["comment-detail.component.scss"],
  styles: [
    `
      .star-detail {
        font-size: 1.2rem;
        color: #b0c4de;
        vertical-align: middle;
        font-style: normal;
      }
      .filled {
        vertical-align: middle;
        color: #ffa200;
      }
      ul.comments li:last-child .media {
        border: none;
      }
    `,
  ],
})
export class CommentDetailComponent implements OnInit {
  @Input() comments: PageModel<CommentModel>;
  @Input() class: string;
  @Output() action = new EventEmitter();
  page = 0;
  public typeDisplayImage = TypeDisplayImage;
  @ViewChild("refComment") refComment: ElementRef;

  constructor() {}

  async ngOnInit() {
    this.page = this.comments.pageIndex + 1;
  }

  getDate(date: string) {
    return new Date(date).toLocaleString();
  }

  pageChange(event) {
    this.action.emit(event);
    window.scrollTo(
      this.refComment.nativeElement.getBoundingClientRect().left +
        window.scrollX,
      this.refComment.nativeElement.getBoundingClientRect().top + window.scrollY
    );
  }
}
