using pz_3_Khvan.Models;
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
	/// <summary>
	/// Логика взаимодействия для Client.xaml
	/// </summary>
	public partial class Client : Page
	{
		private Users _user;
		private string _role;

		public Client(Users user, string role)
		{
			InitializeComponent();

			_user = user;
			_role = role;

			LoadPage();
		}

		public Client()
		{
			InitializeComponent();

			_role = "Гость";

			LoadPage();
		}

		private void LoadPage()
		{
			switch (_role)
			{
				case "Клиент":
					txtWelcome.Text = $"Вы вошли как: {_user.Login}";
					break;

				case "Сотрудник":
				case "Администратор":
				case "Менеджер":
					txtWelcome.Text = $"Вы вошли как: {_user.Login}, (Роль: {_role})";
					break;

				case "Гость":
					txtWelcome.Text = "Вы вошли как гость";
					break;

				default:
					txtWelcome.Text = $"Вы вошли как: {_user.Login}, (Роль: {_role})";
					break;
			}
		}
	}
}
