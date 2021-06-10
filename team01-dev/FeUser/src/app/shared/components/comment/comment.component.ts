import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { BehaviorSubject, Subscriber, Subscription } from "rxjs";
import { CreateCommentModel } from "src/app/lib/data/models/comments/comment.model";
import { AuthService, MessageService } from "src/app/lib/data/services";
import { CommentService } from "src/app/lib/data/services/comments/comment.service";
import { RouterHelperService } from "src/app/lib/helpers";

@Component({
  selector: "app-comment",
  templateUrl: "./comment.component.html",
  providers: [CommentService, AuthService],
  styles: [
    `
      .star {
        font-size: 2rem;
        color: #b0c4de;
      }
      .filled {
        color: #ffa200;
      }
      .checkLogin {
        font-size: 1rem;
        margin: 0;
      }
      .review {
        font-size: 1.4rem;
      }
    `,
  ],
})
export class CommentComponent implements OnInit {
  commentForm: FormGroup;
  @Input() dataComment: CreateCommentModel;
  isLoading: boolean;
  @Output() action = new EventEmitter();
  currentRate = 1;
  subDataUser: Subscription;

  constructor(
    private commentService: CommentService,
    private fb: FormBuilder,
    private messageService: MessageService,
    private routerHelper: RouterHelperService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.subDataUser = this.authService.callUserInfo.subscribe((it) => {
      this.isLoading = Boolean(it);
      this.initForm();
    });
  }

  initForm() {
    this.commentForm = this.fb.group({
      entityId: [this.dataComment.entityId],
      entityType: [this.dataComment.entityType],
      content: [""],
      rating: [this.dataComment.rating],
    });
  }

  Submit() {
    this.commentService
      .create(this.commentForm.value)
      .then((data) => {
        this.initForm();
        this.action.emit();
      })
      .catch((e) => {
        this.messageService.alert(e.error.message);
      });
  }

  returnLogin() {
    this.routerHelper.redirectToLogin();
  }
}
