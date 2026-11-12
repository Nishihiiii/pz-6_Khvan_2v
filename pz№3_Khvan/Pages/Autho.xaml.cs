using pz_3_Khvan.Models;
using pz_3_Khvan.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pz_3_Khvan.Pages
{
	public partial class Autho : Page
	{
		private int click = 0;

		public void ResetFields()
		{
			txtbLogin.Clear();
			pswbPassword.Clear();
			txtboxCaptcha.Clear();

			txtBlockCaptcha.Visibility = Visibility.Hidden;
			txtCaptcha.Visibility = Visibility.Hidden;
			txtboxCaptcha.Visibility = Visibility.Hidden;

			txtCaptcha.Text = "";
			captchaText = "";

			click = 0;
		}

		private string captchaText = "";

		public Autho()
		{
			InitializeComponent();
		}


		private void btnEnterGuests_Click(object sender, RoutedEventArgs e)
		{
			ResetFields();
			NavigationService.Navigate(new Client());
		}

		private void GenerateCaptcha()
		{
			captchaText = CaptchaGenerator.GenerateCaptchaText(6);
			txtCaptcha.TextDecorations = TextDecorations.Strikethrough;

			txtBlockCaptcha.Visibility = Visibility.Visible;
			txtCaptcha.Visibility = Visibility.Visible;
			txtboxCaptcha.Visibility = Visibility.Visible;

			txtCaptcha.Text = captchaText;
			txtboxCaptcha.Clear();
		}

		private void btnEnter_Click(object sender, RoutedEventArgs e)
		{
			string login = txtbLogin.Text.Trim();
			string password = pswbPassword.Password.Trim();

			if (string.IsNullOrWhiteSpace(login) ||
				string.IsNullOrWhiteSpace(password))
			{
				MessageBox.Show("Введите логин и пароль!");
				return;
			}

			if (click > 0)
			{
				if (txtboxCaptcha.Text.Trim() != captchaText)
				{
					MessageBox.Show("Неверно введена CAPTCHA!");

					GenerateCaptcha();
					return;
				}
			}

			try
			{
				using (TelecomServicesDBEntities db =
					   new TelecomServicesDBEntities())
				{

					string passwordHash =
						EncryptionManager.GenerateSha256SecurityKey(password);

					var user = db.Users.FirstOrDefault(x =>
						x.Login == login &&
						x.PasswordHash == passwordHash);

					if (user != null && user.IsActive == true)
					{
						MessageBox.Show("Авторизация прошла успешно!");

						string roleName = user.Roles.RoleName;

						ResetFields();

						NavigationService.Navigate(new Client(user, roleName));
					}
					else
					{
						click++;

						MessageBox.Show(
							"Неверный логин или пароль!");

						if (click == 1)
						{
							GenerateCaptcha();
						}
						else
						{
							GenerateCaptcha();
						}

						pswbPassword.Clear();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					"Ошибка при подключении к базе данных:\n" +
					ex.Message);
			}
		}

	}
}
