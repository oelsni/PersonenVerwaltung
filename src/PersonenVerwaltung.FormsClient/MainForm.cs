using PersonenVerwaltung.ServiceClient;

namespace PersonenVerwaltung.FormsClient
{
	public partial class MainForm : Form
	{
		public MainForm(PersonenVerwaltungClient client)
		{
			InitializeComponent();

			_cmb_Pagesize.SelectedIndex = 2;
			_client = client;
			_persons = Array.Empty<PersonDto>();
			_loadTask = Task.CompletedTask;
			_updateTask = Task.CompletedTask;
			_lsv_Persons.SelectedIndexChanged += _lsv_Persons_SelectedIndexChanged;
			_personDialog = new PersonDialog();
		}


		private static bool ShowError(string title, string message)
		{
			MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			return false;
		}


		private bool ApplyPersons(Page<PersonDto> persons)
		{
			_persons = persons.Items;
			_lsv_Persons.Items.Clear();

			foreach ( PersonDto person in _persons )
				_lsv_Persons.Items.Add(new ListViewItem([$"{person.Id}", person.FamilyName, person.GivenName, $"{person.Birthdate}"]));

			if ( int.Parse(_lbl_Page.Text) == 1 )
			{
				_lbl_Previous.Enabled = false;
				_lbl_Previous.ForeColor = Color.Gray;
			}
			else
			{
				_lbl_Previous.Enabled = true;
				_lbl_Previous.ForeColor = Color.Blue;
			}

			if ( _persons.Count < int.Parse((string) _cmb_Pagesize.SelectedItem!) )
			{
				_lbl_Next.Enabled = false;
				_lbl_Next.ForeColor = Color.Gray;
			}
			else
			{
				_lbl_Next.Enabled = true;
				_lbl_Next.ForeColor = Color.Blue;
			}

			_lsv_Persons.Enabled = true;
			return true;
		}

		private void LoadPersonsContinuation(Task<ApiResult<Page<PersonDto>>> loadTask)
		{
			if ( InvokeRequired )
			{
				Invoke(LoadPersonsContinuation, loadTask);
				return;
			}

			bool success = loadTask.Result switch
			{
				Page<PersonDto> ps => ApplyPersons(ps),
				ProblemDetails pd => ShowError(pd.Title, pd.Detail),
				Exception err => ShowError("Error", err.Message),
			};
		}

		private void UpdatePersonContinuation(Task<ApiResult> updateTask)
		{
			if ( InvokeRequired )
			{
				Invoke(UpdatePersonContinuation, updateTask);
				return;
			}

			_btn_Load_Click(_btn_Load, EventArgs.Empty);
		}


		private void _btn_Load_Click(object? sender, EventArgs e)
		{
			_lsv_Persons.Enabled = false;

			if ( !string.IsNullOrWhiteSpace(_txb_Filter.Text) )
				_loadTask = _client.ListPersonsAsync(_txb_Filter.Text, int.Parse(_lbl_Page.Text) - 1, int.Parse((string) _cmb_Pagesize.SelectedItem!)).ContinueWith(LoadPersonsContinuation);
			else
				_loadTask = _client.ListPersonsAsync(page: int.Parse(_lbl_Page.Text) - 1, pagesize: int.Parse((string) _cmb_Pagesize.SelectedItem!)).ContinueWith(LoadPersonsContinuation);
		}

		private void _lbl_Previuos_Click(object? sender, EventArgs e)
		{
			if ( _persons.Count == 0 )
				return;

			if ( int.Parse(_lbl_Page.Text) == 1 )
				return;
		}

		private void _lbl_Next_Click(object? sender, EventArgs e)
		{
			if ( _persons.Count == 0 )
				return;

			if ( _persons.Count < int.Parse((string) _cmb_Pagesize.SelectedItem!) )
				return;
		}

		private void _lsv_Persons_SelectedIndexChanged(object? sender, EventArgs e)
		{
			if ( _lsv_Persons.SelectedItems.Count != 1 || !int.TryParse(_lsv_Persons.SelectedItems[0].Text, out int personId) )
				return;

			PersonDto person = _persons.Single(e => e.Id == personId);

			_personDialog.Person = person;

			if ( _personDialog.ShowDialog() == DialogResult.Continue )
			{
				_lsv_Persons.Enabled = false;
				_updateTask = _client.UpdatePersonAsync(person.Id, _personDialog.NameUpdate).ContinueWith(UpdatePersonContinuation);
			}
		}


		readonly PersonenVerwaltungClient
			_client;

		readonly PersonDialog
			_personDialog;

		IReadOnlyList<PersonDto>
			_persons;

		Task
			_loadTask,
			_updateTask;
	}
}
