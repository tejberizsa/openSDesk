import { Component, OnInit } from '@angular/core';
import { Confirm } from 'src/app/_models/confirm';
import { AuthService } from 'src/app/_services/auth.service';
import { Router, ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-user-confirm',
  templateUrl: './user-confirm.component.html',
  styleUrls: ['./user-confirm.component.css']
})
export class UserConfirmComponent implements OnInit {
  confirm: Confirm = { id: '', confirmkey: ''};

  constructor(private authService: AuthService, private router: Router, private route: ActivatedRoute,
    private alertify: AlertifyService) { }

  ngOnInit() {
    this.confirm.id = this.route.snapshot.params['id'];
    this.confirm.confirmkey = this.route.snapshot.params['ckey'];
    this.confirmMail();
  }

  confirmMail() {
    this.authService.confirmEmail(this.confirm).subscribe(() => {
      this.alertify.success('Sikeres bejelentkezés');
      this.router.navigate(['/home']);
    }, error => {
      this.alertify.error(error);
      this.router.navigate(['/home']);
    });
  }
}
