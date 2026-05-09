using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeAreCars
{
    /// <summary>
    /// Main application form implementing the WeAreCars rental management system.
    /// Displays a welcome splash screen, authentication panel, and booking management interface.
    /// </summary>
    public partial class Form1 : Form
    {
        // Form panels for different screens
        private Panel splashPanel;
        private Panel loginPanel;
        private Panel mainPanel;
        private Panel summaryPanel;

        // Login controls
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblLoginError;

        // Booking form controls
        private TextBox txtFirstName;
        private TextBox txtSurname;
        private TextBox txtAddress;
        private TextBox txtAge;
        private CheckBox chkValidLicense;
        private NumericUpDown numDays;
        private ComboBox cbCarType;
        private ComboBox cbFuelType;
        private CheckBox chkUnlimitedMileage;
        private CheckBox chkBreakdownCover;
        private Button btnProcessBooking;
        private Label lblPricePreview;
        private DataGridView dgvRentedCars;

        // Summary controls
        private Label lblSummaryDetails;
        private Label lblSummaryTotal;
        private Button btnConfirmBooking;
        private Button btnCancelBooking;

        private Models.User loggedInUser;
        private int loginAttempts = 0;
        private Models.CarBooking currentBooking;

        public Form1()
        {
            InitializeComponent();
            SetupUI();
        }

        /// <summary>
        /// Initializes all UI panels and controls for the application.
        /// </summary>
        private void SetupUI()
        {
            this.Text = "WeAreCars - Vehicle Rental Management System";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 240);

            // Create splash screen first
            CreateSplashScreen();
            // Create login panel
            CreateLoginPanel();
            // Create main booking panel
            CreateMainPanel();
            // Create summary panel
            CreateSummaryPanel();

            // Initially show splash screen
            splashPanel.Visible = true;
            loginPanel.Visible = false;
            mainPanel.Visible = false;
            summaryPanel.Visible = false;

            this.Controls.Add(splashPanel);
            this.Controls.Add(loginPanel);
            this.Controls.Add(mainPanel);
            this.Controls.Add(summaryPanel);
        }

        /// <summary>
        /// Creates the welcome splash screen with instructions.
        /// </summary>
        private void CreateSplashScreen()
        {
            splashPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(41, 128, 185)
            };

            var lblTitle = new Label
            {
                Text = "Welcome to WeAreCars",
                Font = new Font("Arial", 36, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(150, 100),
                AutoSize = true
            };

            var lblSubtitle = new Label
            {
                Text = "Professional Vehicle Rental Management System",
                Font = new Font("Arial", 14),
                ForeColor = Color.White,
                Location = new Point(150, 160),
                AutoSize = true
            };

            var lblInstructions = new Label
            {
                Text = "System Instructions:\n\n" +
                       "1. Log in with your staff credentials\n" +
                       "2. View currently rented vehicles\n" +
                       "3. Enter customer details and select vehicle preferences\n" +
                       "4. Review the booking summary before confirmation\n" +
                       "5. Complete the rental transaction\n\n" +
                       "All fields marked with * are mandatory.",
                Font = new Font("Arial", 11),
                ForeColor = Color.White,
                Location = new Point(150, 220),
                Size = new Size(700, 250),
                AutoSize = false
            };

            var btnProceedToLogin = new Button
            {
                Text = "Proceed to Login",
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                Location = new Point(350, 520),
                Size = new Size(300, 50),
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat
            };
            btnProceedToLogin.Click += (s, e) =>
            {
                splashPanel.Visible = false;
                loginPanel.Visible = true;
            };

            splashPanel.Controls.Add(lblTitle);
            splashPanel.Controls.Add(lblSubtitle);
            splashPanel.Controls.Add(lblInstructions);
            splashPanel.Controls.Add(btnProceedToLogin);
        }

        /// <summary>
        /// Creates the staff login panel with authentication controls.
        /// </summary>
        private void CreateLoginPanel()
        {
            loginPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            var pnlLoginBox = new Panel
            {
                Location = new Point(300, 150),
                Size = new Size(400, 350),
                BackColor = Color.FromArgb(245, 245, 245),
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblLoginTitle = new Label
            {
                Text = "Staff Login",
                Font = new Font("Arial", 18, FontStyle.Bold),
                Location = new Point(100, 20),
                AutoSize = true,
                ForeColor = Color.FromArgb(41, 128, 185)
            };

            var lblUsername = new Label { Text = "Username:", Location = new Point(40, 80), AutoSize = true, Font = new Font("Arial", 10) };
            txtUsername = new TextBox { Location = new Point(40, 105), Width = 320, Height = 35, Font = new Font("Arial", 11), Padding = new Padding(5) };

            var lblPassword = new Label { Text = "Password:", Location = new Point(40, 160), AutoSize = true, Font = new Font("Arial", 10) };
            txtPassword = new TextBox
            {
                Location = new Point(40, 185),
                Width = 320,
                Height = 35,
                PasswordChar = '*',
                Font = new Font("Arial", 11),
                Padding = new Padding(5)
            };

            btnLogin = new Button
            {
                Text = "Login",
                Location = new Point(40, 240),
                Width = 320,
                Height = 40,
                Font = new Font("Arial", 11, FontStyle.Bold),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnLogin.Click += BtnLogin_Click;

            lblLoginError = new Label
            {
                Location = new Point(40, 290),
                Width = 320,
                ForeColor = Color.Red,
                Font = new Font("Arial", 9),
                AutoSize = true
            };

            pnlLoginBox.Controls.Add(lblLoginTitle);
            pnlLoginBox.Controls.Add(lblUsername);
            pnlLoginBox.Controls.Add(txtUsername);
            pnlLoginBox.Controls.Add(lblPassword);
            pnlLoginBox.Controls.Add(txtPassword);
            pnlLoginBox.Controls.Add(btnLogin);
            pnlLoginBox.Controls.Add(lblLoginError);

            loginPanel.Controls.Add(pnlLoginBox);

            ToolTip ttLogin = new ToolTip();
            ttLogin.SetToolTip(txtUsername, "Enter staff username (Demo: sta001)");
            ttLogin.SetToolTip(txtPassword, "Enter staff password (Demo: givemethekeys123)");
        }

        /// <summary>
        /// Creates the main booking panel with all rental options.
        /// </summary>
        private void CreateMainPanel()
        {
            mainPanel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, AutoScroll = true };

            // Header
            var pnlHeader = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(1000, 60),
                BackColor = Color.FromArgb(41, 128, 185),
                Dock = DockStyle.Top
            };

            var lblWelcome = new Label
            {
                Text = "Booking Management System",
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 15),
                AutoSize = true
            };

            var btnLogout = new Button
            {
                Text = "Logout",
                Location = new Point(850, 10),
                Size = new Size(120, 40),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            btnLogout.Click += (s, e) =>
            {
                loggedInUser = null;
                loginPanel.Visible = true;
                mainPanel.Visible = false;
                summaryPanel.Visible = false;
                txtUsername.Clear();
                txtPassword.Clear();
                lblLoginError.Text = "";
                loginAttempts = 0;
            };

            pnlHeader.Controls.Add(lblWelcome);
            pnlHeader.Controls.Add(btnLogout);
            mainPanel.Controls.Add(pnlHeader);

            // Rented Cars Section
            var lblRentedCars = new Label
            {
                Text = "Currently Rented Vehicles",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(20, 70),
                AutoSize = true,
                ForeColor = Color.FromArgb(41, 128, 185)
            };

            dgvRentedCars = new DataGridView
            {
                Location = new Point(20, 95),
                Size = new Size(950, 150),
                ReadOnly = true,
                AllowUserToAddRows = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            var btnRefreshRentals = new Button
            {
                Text = "Refresh List",
                Location = new Point(20, 250),
                Size = new Size(100, 30),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnRefreshRentals.Click += (s, e) => UpdateRentedCarsList();

            // Booking Form Section
            var lblBookingForm = new Label
            {
                Text = "New Booking Details",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(20, 290),
                AutoSize = true,
                ForeColor = Color.FromArgb(41, 128, 185)
            };

            ToolTip ttMain = new ToolTip();
            int yPos = 320;
            int leftCol = 20;
            int rightCol = 500;

            // Customer Details - Left Column
            mainPanel.Controls.Add(new Label { Text = "Customer First Name*", Location = new Point(leftCol, yPos), Font = new Font("Arial", 10, FontStyle.Bold) });
            txtFirstName = new TextBox { Location = new Point(leftCol, yPos + 25), Width = 450, Height = 30, Font = new Font("Arial", 10) };
            ttMain.SetToolTip(txtFirstName, "Enter customer's first name (required)");
            mainPanel.Controls.Add(txtFirstName);
            yPos += 60;

            mainPanel.Controls.Add(new Label { Text = "Customer Surname*", Location = new Point(leftCol, yPos), Font = new Font("Arial", 10, FontStyle.Bold) });
            txtSurname = new TextBox { Location = new Point(leftCol, yPos + 25), Width = 450, Height = 30, Font = new Font("Arial", 10) };
            ttMain.SetToolTip(txtSurname, "Enter customer's surname (required)");
            mainPanel.Controls.Add(txtSurname);
            yPos += 60;

            mainPanel.Controls.Add(new Label { Text = "Customer Address*", Location = new Point(leftCol, yPos), Font = new Font("Arial", 10, FontStyle.Bold) });
            txtAddress = new TextBox { Location = new Point(leftCol, yPos + 25), Width = 450, Height = 30, Font = new Font("Arial", 10) };
            ttMain.SetToolTip(txtAddress, "Enter customer's full address (required)");
            mainPanel.Controls.Add(txtAddress);
            yPos += 60;

            mainPanel.Controls.Add(new Label { Text = "Customer Age*", Location = new Point(leftCol, yPos), Font = new Font("Arial", 10, FontStyle.Bold) });
            txtAge = new TextBox { Location = new Point(leftCol, yPos + 25), Width = 200, Height = 30, Font = new Font("Arial", 10) };
            ttMain.SetToolTip(txtAge, "Enter customer's age (must be 18+, required)");
            mainPanel.Controls.Add(txtAge);
            yPos += 60;

            chkValidLicense = new CheckBox
            {
                Text = "Valid Driving License*",
                Location = new Point(leftCol, yPos),
                AutoSize = true,
                Font = new Font("Arial", 10)
            };
            ttMain.SetToolTip(chkValidLicense, "Customer must have valid driving license (required)");
            mainPanel.Controls.Add(chkValidLicense);

            // Rental Details - Right Column
            yPos = 320;

            mainPanel.Controls.Add(new Label { Text = "Rental Duration (Days)*", Location = new Point(rightCol, yPos), Font = new Font("Arial", 10, FontStyle.Bold) });
            numDays = new NumericUpDown
            {
                Location = new Point(rightCol, yPos + 25),
                Width = 200,
                Height = 30,
                Minimum = 1,
                Maximum = 28,
                Value = 1,
                Font = new Font("Arial", 10)
            };
            ttMain.SetToolTip(numDays, "Select rental duration (1-28 days at £25/day, required)");
            numDays.ValueChanged += UpdatePricePreview;
            mainPanel.Controls.Add(numDays);
            yPos += 60;

            mainPanel.Controls.Add(new Label { Text = "Car Type*", Location = new Point(rightCol, yPos), Font = new Font("Arial", 10, FontStyle.Bold) });
            cbCarType = new ComboBox
            {
                Location = new Point(rightCol, yPos + 25),
                Width = 450,
                Height = 30,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Arial", 10)
            };
            cbCarType.Items.AddRange(new object[]
            {
                "City Car (No extra charge)",
                "Family Car (+£50)",
                "Sports Car (+£75)",
                "SUV (+£65)"
            });
            cbCarType.SelectedIndex = 0;
            ttMain.SetToolTip(cbCarType, "Select desired car type (required)");
            cbCarType.SelectedIndexChanged += UpdatePricePreview;
            mainPanel.Controls.Add(cbCarType);
            yPos += 60;

            mainPanel.Controls.Add(new Label { Text = "Fuel Type*", Location = new Point(rightCol, yPos), Font = new Font("Arial", 10, FontStyle.Bold) });
            cbFuelType = new ComboBox
            {
                Location = new Point(rightCol, yPos + 25),
                Width = 450,
                Height = 30,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Arial", 10)
            };
            cbFuelType.Items.AddRange(new object[]
            {
                "Petrol (No extra charge)",
                "Diesel (No extra charge)",
                "Hybrid (+£30)",
                "Full Electric (+£50)"
            });
            cbFuelType.SelectedIndex = 0;
            ttMain.SetToolTip(cbFuelType, "Select preferred fuel type (required)");
            cbFuelType.SelectedIndexChanged += UpdatePricePreview;
            mainPanel.Controls.Add(cbFuelType);
            yPos += 60;

            // Extras Section
            var lblExtras = new Label
            {
                Text = "Optional Extras",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Location = new Point(rightCol, yPos),
                ForeColor = Color.FromArgb(155, 89, 182)
            };
            mainPanel.Controls.Add(lblExtras);
            yPos += 30;

            chkUnlimitedMileage = new CheckBox
            {
                Text = "Unlimited Mileage (+£10 per day)",
                Location = new Point(rightCol, yPos),
                AutoSize = true,
                Font = new Font("Arial", 10)
            };
            ttMain.SetToolTip(chkUnlimitedMileage, "Add unlimited mileage option");
            chkUnlimitedMileage.CheckedChanged += UpdatePricePreview;
            mainPanel.Controls.Add(chkUnlimitedMileage);
            yPos += 30;

            chkBreakdownCover = new CheckBox
            {
                Text = "Breakdown Cover (+£2 per day)",
                Location = new Point(rightCol, yPos),
                AutoSize = true,
                Font = new Font("Arial", 10)
            };
            ttMain.SetToolTip(chkBreakdownCover, "Add roadside breakdown cover");
            chkBreakdownCover.CheckedChanged += UpdatePricePreview;
            mainPanel.Controls.Add(chkBreakdownCover);

            // Price Preview and Process Button
            yPos = 650;

            lblPricePreview = new Label
            {
                Location = new Point(leftCol, yPos - 50),
                Width = 450,
                Height = 80,
                BackColor = Color.FromArgb(240, 240, 240),
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Arial", 10),
                Padding = new Padding(10),
                AutoSize = false
            };
            mainPanel.Controls.Add(lblPricePreview);

            btnProcessBooking = new Button
            {
                Text = "Review & Process Booking",
                Location = new Point(rightCol, yPos - 40),
                Size = new Size(450, 50),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                Font = new Font("Arial", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnProcessBooking.Click += BtnProcessBooking_Click;
            mainPanel.Controls.Add(btnProcessBooking);

            mainPanel.Controls.Add(lblRentedCars);
            mainPanel.Controls.Add(dgvRentedCars);
            mainPanel.Controls.Add(btnRefreshRentals);
            mainPanel.Controls.Add(lblBookingForm);

            mainPanel.Padding = new Padding(0, 60, 0, 0);
        }

        /// <summary>
        /// Creates the booking summary confirmation panel.
        /// </summary>
        private void CreateSummaryPanel()
        {
            summaryPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                AutoScroll = true
            };

            var pnlSummary = new Panel
            {
                Location = new Point(150, 50),
                Size = new Size(700, 550),
                BackColor = Color.FromArgb(245, 245, 245),
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblSummaryTitle = new Label
            {
                Text = "Booking Summary",
                Font = new Font("Arial", 18, FontStyle.Bold),
                Location = new Point(200, 20),
                AutoSize = true,
                ForeColor = Color.FromArgb(41, 128, 185)
            };

            lblSummaryDetails = new Label
            {
                Location = new Point(40, 70),
                Width = 620,
                Height = 350,
                Font = new Font("Arial", 11),
                AutoSize = false
            };

            lblSummaryTotal = new Label
            {
                Location = new Point(40, 430),
                Width = 620,
                Height = 40,
                Font = new Font("Arial", 13, FontStyle.Bold),
                ForeColor = Color.FromArgb(46, 204, 113),
                AutoSize = false
            };

            btnConfirmBooking = new Button
            {
                Text = "Confirm & Save Booking",
                Location = new Point(100, 490),
                Size = new Size(250, 45),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                Font = new Font("Arial", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnConfirmBooking.Click += BtnConfirmBooking_Click;

            btnCancelBooking = new Button
            {
                Text = "Cancel & Return",
                Location = new Point(380, 490),
                Size = new Size(250, 45),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                Font = new Font("Arial", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancelBooking.Click += (s, e) =>
            {
                summaryPanel.Visible = false;
                mainPanel.Visible = true;
            };

            pnlSummary.Controls.Add(lblSummaryTitle);
            pnlSummary.Controls.Add(lblSummaryDetails);
            pnlSummary.Controls.Add(lblSummaryTotal);
            pnlSummary.Controls.Add(btnConfirmBooking);
            pnlSummary.Controls.Add(btnCancelBooking);

            summaryPanel.Controls.Add(pnlSummary);
        }

        /// <summary>
        /// Handles staff login authentication with attempt limiting.
        /// </summary>
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (loginAttempts >= 3)
            {
                lblLoginError.Text = "Account locked. Contact administrator.";
                btnLogin.Enabled = false;
                return;
            }

            try
            {
                using (var conn = Data.DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT UserID, Username FROM Users WHERE Username = @user AND Password = @pass";
                    using (var cmd = new System.Data.SqlClient.SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@pass", txtPassword.Text);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int userId = reader.GetInt32(0);
                                string username = reader.GetString(1);

                                loggedInUser = new Models.User { Id = userId, Username = username };
                                loginPanel.Visible = false;
                                mainPanel.Visible = true;
                                UpdateRentedCarsList();
                                UpdatePricePreview(null, null);
                                loginAttempts = 0;
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblLoginError.Text = "Database error: " + ex.Message;
                return;
            }

            loginAttempts++;
            int remaining = 3 - loginAttempts;
            if (remaining == 0)
            {
                lblLoginError.Text = "Maximum attempts reached. Account locked.";
                btnLogin.Enabled = false;
            }
            else
            {
                lblLoginError.Text = $"Invalid credentials. {remaining} attempt(s) remaining.";
            }
        }

        /// <summary>
        /// Validates booking details and displays the summary confirmation screen.
        /// </summary>
        private void BtnProcessBooking_Click(object sender, EventArgs e)
        {
            if (!ValidateBookingDetails()) return;

            currentBooking = new Models.CarBooking
            {
                CustomerFirstName = txtFirstName.Text,
                CustomerSurname = txtSurname.Text,
                CustomerAddress = txtAddress.Text,
                CustomerAge = int.Parse(txtAge.Text),
                HasValidDrivingLicense = chkValidLicense.Checked,
                NumberOfDays = (int)numDays.Value,
                SelectedCarType = (Models.CarType)cbCarType.SelectedIndex,
                SelectedFuelType = (Models.FuelType)cbFuelType.SelectedIndex,
                UnlimitedMileage = chkUnlimitedMileage.Checked,
                BreakdownCover = chkBreakdownCover.Checked,
                StaffID = loggedInUser.Id,
                BookingDate = DateTime.Now
            };

            currentBooking.TotalCost = currentBooking.CalculateTotalCost();

            // Display summary
            string summary = $"BOOKING DETAILS SUMMARY\n" +
                           $"========================\n\n" +
                           $"CUSTOMER INFORMATION:\n" +
                           $"Name: {currentBooking.CustomerFirstName} {currentBooking.CustomerSurname}\n" +
                           $"Address: {currentBooking.CustomerAddress}\n" +
                           $"Age: {currentBooking.CustomerAge} years\n" +
                           $"Valid License: {(currentBooking.HasValidDrivingLicense ? "Yes" : "No")}\n\n" +
                           $"RENTAL DETAILS:\n" +
                           $"Duration: {currentBooking.NumberOfDays} days @ £25/day\n" +
                           $"Car Type: {cbCarType.SelectedItem}\n" +
                           $"Fuel Type: {cbFuelType.SelectedItem}\n\n" +
                           $"OPTIONAL EXTRAS:\n" +
                           $"Unlimited Mileage: {(currentBooking.UnlimitedMileage ? "Yes (+£10/day)" : "No")}\n" +
                           $"Breakdown Cover: {(currentBooking.BreakdownCover ? "Yes (+£2/day)" : "No")}\n\n" +
                           $"Staff Member: {loggedInUser.Username}\n" +
                           $"Date Processed: {currentBooking.BookingDate:dd/MM/yyyy HH:mm:ss}";

            lblSummaryDetails.Text = summary;
            lblSummaryTotal.Text = $"TOTAL COST: £{currentBooking.TotalCost:0.00}";

            mainPanel.Visible = false;
            summaryPanel.Visible = true;
        }

        /// <summary>
        /// Saves the confirmed booking to the database.
        /// </summary>
        private void BtnConfirmBooking_Click(object sender, EventArgs e)
        {
            try
            {
                SaveBookingToDatabase(currentBooking);
                MessageBox.Show(
                    $"Booking successfully saved!\n\nBooking ID: {currentBooking.Id}\nTotal Cost: £{currentBooking.TotalCost:0.00}",
                    "Booking Confirmed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                summaryPanel.Visible = false;
                mainPanel.Visible = true;
                UpdateRentedCarsList();
                ClearBookingForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving booking: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Saves the booking to the database using parameterized SQL queries.
        /// </summary>
        private void SaveBookingToDatabase(Models.CarBooking booking)
        {
            using (var conn = Data.DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = @"
                    INSERT INTO Car_Booking 
                    (CustomerFirstName, CustomerSurname, CustomerAddress, CustomerAge, 
                     HasValidDrivingLicense, NumberOfDays, CarType, FuelType, 
                     HasUnlimitedMileage, HasBreakdownCover, TotalCost, StaffID, BookingDate)
                    VALUES 
                    (@FirstName, @Surname, @Address, @Age, @License, @Days, 
                     @CarType, @FuelType, @Mileage, @Breakdown, @TotalCost, @StaffID, @Date)";

                using (var cmd = new System.Data.SqlClient.SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", booking.CustomerFirstName);
                    cmd.Parameters.AddWithValue("@Surname", booking.CustomerSurname);
                    cmd.Parameters.AddWithValue("@Address", booking.CustomerAddress);
                    cmd.Parameters.AddWithValue("@Age", booking.CustomerAge);
                    cmd.Parameters.AddWithValue("@License", booking.HasValidDrivingLicense);
                    cmd.Parameters.AddWithValue("@Days", booking.NumberOfDays);
                    cmd.Parameters.AddWithValue("@CarType", (int)booking.SelectedCarType);
                    cmd.Parameters.AddWithValue("@FuelType", (int)booking.SelectedFuelType);
                    cmd.Parameters.AddWithValue("@Mileage", booking.UnlimitedMileage);
                    cmd.Parameters.AddWithValue("@Breakdown", booking.BreakdownCover);
                    cmd.Parameters.AddWithValue("@TotalCost", booking.TotalCost);
                    cmd.Parameters.AddWithValue("@StaffID", booking.StaffID);
                    cmd.Parameters.AddWithValue("@Date", booking.BookingDate);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Validates all required booking fields.
        /// </summary>
        private bool ValidateBookingDetails()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Please enter customer first name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSurname.Text))
            {
                MessageBox.Show("Please enter customer surname.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Please enter customer address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAge.Text) || !int.TryParse(txtAge.Text, out int age) || age < 18)
            {
                MessageBox.Show("Please enter a valid age (18 or older).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!chkValidLicense.Checked)
            {
                MessageBox.Show("Customer must have a valid driving license to proceed.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Updates the price preview based on current selections.
        /// </summary>
        private void UpdatePricePreview(object sender, EventArgs e)
        {
            int days = (int)numDays.Value;
            decimal baseRate = 25m;
            decimal totalCost = days * baseRate;
            string calculation = $"Base Rate: {days} days × £25 = £{totalCost:0.00}\n";

            switch ((Models.CarType)cbCarType.SelectedIndex)
            {
                case Models.CarType.FamilyCar:
                    totalCost += 50m;
                    calculation += $"Family Car: +£50.00\n";
                    break;
                case Models.CarType.SportsCar:
                    totalCost += 75m;
                    calculation += $"Sports Car: +£75.00\n";
                    break;
                case Models.CarType.SUV:
                    totalCost += 65m;
                    calculation += $"SUV: +£65.00\n";
                    break;
            }

            switch ((Models.FuelType)cbFuelType.SelectedIndex)
            {
                case Models.FuelType.Hybrid:
                    totalCost += 30m;
                    calculation += $"Hybrid Fuel: +£30.00\n";
                    break;
                case Models.FuelType.FullElectric:
                    totalCost += 50m;
                    calculation += $"Electric Fuel: +£50.00\n";
                    break;
            }

            if (chkUnlimitedMileage.Checked)
            {
                decimal cost = 10m * days;
                totalCost += cost;
                calculation += $"Unlimited Mileage: +£{cost:0.00}\n";
            }

            if (chkBreakdownCover.Checked)
            {
                decimal cost = 2m * days;
                totalCost += cost;
                calculation += $"Breakdown Cover: +£{cost:0.00}\n";
            }

            calculation += $"\n════════════════════\n";
            calculation += $"TOTAL: £{totalCost:0.00}";

            lblPricePreview.Text = calculation;
        }

        /// <summary>
        /// Updates the list of currently rented vehicles from the database.
        /// </summary>
        private void UpdateRentedCarsList()
        {
            dgvRentedCars.DataSource = null;

            try
            {
                var rentedCars = new List<object>();

                using (var conn = Data.DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT BookingID, CONCAT(CustomerFirstName, ' ', CustomerSurname) as CustomerName, 
                               NumberOfDays, CarType, TotalCost, BookingDate
                        FROM Car_Booking
                        ORDER BY BookingDate DESC";

                    using (var cmd = new System.Data.SqlClient.SqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string carTypeName = ((Models.CarType)reader.GetInt32(3)).ToString();
                            rentedCars.Add(new
                            {
                                BookingID = reader.GetInt32(0),
                                Customer = reader.GetString(1),
                                Days = reader.GetInt32(2),
                                CarType = carTypeName,
                                Cost = $"£{reader.GetDecimal(4):0.00}",
                                Date = reader.GetDateTime(5).ToString("dd/MM/yyyy HH:mm")
                            });
                        }
                    }
                }

                dgvRentedCars.DataSource = rentedCars;
                dgvRentedCars.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading rental list: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Clears all booking form fields.
        /// </summary>
        private void ClearBookingForm()
        {
            txtFirstName.Clear();
            txtSurname.Clear();
            txtAddress.Clear();
            txtAge.Clear();
            chkValidLicense.Checked = false;
            numDays.Value = 1;
            cbCarType.SelectedIndex = 0;
            cbFuelType.SelectedIndex = 0;
            chkUnlimitedMileage.Checked = false;
            chkBreakdownCover.Checked = false;
            UpdatePricePreview(null, null);
        }
    }
}
