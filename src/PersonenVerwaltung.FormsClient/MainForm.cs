using PersonenVerwaltung.ServiceClient;

namespace PersonenVerwaltung.FormsClient
{
	public partial class MainForm : Form
	{
		public MainForm(PersonenVerwaltungClient client)
		{
			InitializeComponent();

			_persons = Array.Empty<PersonDto>();
			_loadTask = Task.CompletedTask;
			_updateTask = Task.CompletedTask;
			_personDialog = new PersonDialog();
			_client = client;
			_lsv_Persons.SelectedIndexChanged += _lsv_Persons_SelectedIndexChanged;
			_cmb_Pagesize.SelectedIndex = 2;
			_lastFilter = _txb_Filter.Text;

			ToolTip toolTip1 = new ToolTip
			{
				AutoPopDelay = 10000,
				InitialDelay = 500,
				ReshowDelay = 200,
				ShowAlways = true
			};

			toolTip1.SetToolTip(_txb_Filter, "Use wildcards for filtering (EndsWith: *xxx / StartsWith: xxx* / Contains: *xxx*)");
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
			int page = int.Parse(_lbl_Page.Text);
			int pageSize = int.Parse((string) _cmb_Pagesize.SelectedItem!);
			int last = page * pageSize;

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

			if ( _persons.Count < int.Parse((string) _cmb_Pagesize.SelectedItem!) || last >= persons.TotalCount )
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
			_cmb_Pagesize.Enabled = true;
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
			_cmb_Pagesize.Enabled = false;

			if ( _lastFilter != _txb_Filter.Text )
				_lbl_Page.Text = "1";

			_lastFilter = _txb_Filter.Text;

			if ( !string.IsNullOrWhiteSpace(_txb_Filter.Text) )
				_loadTask = _client.ListPersonsAsync(_txb_Filter.Text, int.Parse(_lbl_Page.Text) - 1, int.Parse((string) _cmb_Pagesize.SelectedItem!)).ContinueWith(LoadPersonsContinuation);
			else
				_loadTask = _client.ListPersonsAsync(page: int.Parse(_lbl_Page.Text) - 1, pagesize: int.Parse((string) _cmb_Pagesize.SelectedItem!)).ContinueWith(LoadPersonsContinuation);
		}

		private void _cmb_Pagesize_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ( _persons.Count == 0 )
				return;

			_lbl_Page.Text = "1";
			_btn_Load_Click(_btn_Load, EventArgs.Empty);
		}

		private void _lbl_Previuos_Click(object? sender, EventArgs e)
		{
			if ( int.Parse(_lbl_Page.Text) == 1 )
			{
				_lbl_Previous.Enabled = false;
				_lbl_Previous.ForeColor = Color.Gray;
				return;
			}

			_lbl_Page.Text = $"{int.Parse(_lbl_Page.Text) - 1}";
			_btn_Load_Click(_btn_Load, EventArgs.Empty);
		}

		private void _lbl_Next_Click(object? sender, EventArgs e)
		{
			if ( _persons.Count == 0 )
			{
				_lbl_Next.Enabled = false;
				_lbl_Next.ForeColor = Color.Gray;
				return;
			}

			if ( _persons.Count < int.Parse((string) _cmb_Pagesize.SelectedItem!) )
			{
				_lbl_Next.Enabled = false;
				_lbl_Next.ForeColor = Color.Gray;
				return;
			}

			_lbl_Page.Text = $"{int.Parse(_lbl_Page.Text) + 1}";
			_btn_Load_Click(_btn_Load, EventArgs.Empty);
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

		string
			_lastFilter;
	}
}
