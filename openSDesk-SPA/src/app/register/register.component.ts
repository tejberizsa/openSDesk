import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { User } from '../_models/user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  user: User;
  registerForm: FormGroup;
  minDate = new Date();
  maxDate = new Date();

  constructor(private authService: AuthService, private router: Router,
    private alertify: AlertifyService, private fb: FormBuilder) { }

  ngOnInit() {
    this.minDate.setFullYear(this.minDate.getFullYear() - 18);
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 99);
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      gender: ['male'],
      birth: ['', Validators.required],
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(12)]],
      confirmPassword: ['', Validators.required]
    }, {validator: this.passwordMatchValidator});
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value ? null : {'mismatch': true};
  }

  // ageMinValidator(g: FormGroup) {
  //   console.log('validating');
  //   console.log(g.get('birth').value);
  //   console.log(this.minDate);
  //   return this.registerForm.get('birth').value <= this.minDate ? null : {'mismatch': true};
  // }

  // ageMaxValidator(g: FormGroup) {
  //   return g.get('birth').value >= this.maxDate ? null : {'mismatch': true};
  // }

  register() {
    if (this.registerForm.valid) {
      this.user = Object.assign({}, this.registerForm.value);
      this.authService.register(this.user).subscribe(() => {
        this.alertify.warning('Kérlek igazold vissza az e-mail címed, szükséges linket elküdtük a megadott címre!');
        this.alertify.success('Sikeres regisztráció');
        this.router.navigate(['/home']);
      }, error => {
        this.alertify.error(error);
      });
    }
  }

  cancel() {
  }

}
