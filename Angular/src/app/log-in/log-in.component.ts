import { Component } from '@angular/core';
import {FormControl, FormBuilder, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.scss']
})
export class LogInComponent {
  LogInForma:FormGroup;
  submitted=false;
  success=false;
  username:any;
  password:any;


  constructor(private formBuilder:FormBuilder) {

    this.LogInForma=this.formBuilder.group({
      username:new FormControl('', Validators.required),
      password:new FormControl('', Validators.required)
      })
  }

  onSubmit()
  {
    this.submitted=true;

    if(this.LogInForma.invalid)
    {
      return;
    }
      this.success=true;

  }

  ngOnInit()
  {

  }

}
