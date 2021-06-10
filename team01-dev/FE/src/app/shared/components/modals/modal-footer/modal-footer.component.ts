import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { ModalFooterModel } from '../models/modal.model';

@Component({
  selector: 'app-modal-footer',
  templateUrl: './modal-footer.component.html',
  styleUrls: ['./modal-footer.component.scss']
})
export class ModalFooterComponent implements OnInit {
  @Input() data = new ModalFooterModel();
  @Output() onAction = new EventEmitter();
  constructor() { }
  ngOnInit() {
  }
  action() {
    this.onAction.emit("ok");
  }
}