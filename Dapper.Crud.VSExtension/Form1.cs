﻿using Dapper.Crud.VSExtension.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ScintillaNET;

namespace Dapper.Crud.VSExtension
{
    public partial class Form1 : Form
    {
        public string Projectpath;

        public Form1()
        {
            InitializeComponent();
            SetTxtStyles();
        }

        private IEnumerable<string> FilterFileList(List<string> files)
        {
            files.RemoveAll(x => x.Contains("TemporaryGeneratedFile"));
            files.RemoveAll(x => x.Contains("AssemblyInfo.cs"));
            files.RemoveAll(x => x.Contains("Program.cs"));
            files.RemoveAll(x => x.Contains("Startup.cs"));
            return files;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lstFiles.Items.Clear();
            var project = ProjectHelpers.GetActiveProject();

            Projectpath = project.GetFullPath();

            var files = Directory.GetFiles(Projectpath, "*.cs", SearchOption.AllDirectories).ToList();
            var filteredList = FilterFileList(files);

            foreach (var file in filteredList)
            {
                lstFiles.Items.Add(file.Replace(Projectpath, "").Replace(".cs", ""));
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            foreach (var item in lstFiles.Items)
            {
                txtOutput.Text += DapperGenerator.Select(item.ToString());
            }
        }

        private void SetTxtStyles()
        {
            txtOutput.StyleResetDefault();
            txtOutput.Styles[Style.Default].Font = "Consolas";
            txtOutput.Styles[Style.Default].Size = 10;
            txtOutput.StyleClearAll();

            // Configure the CPP (C#) lexer styles
            txtOutput.Styles[Style.Cpp.Default].ForeColor = Color.Silver;
            txtOutput.Styles[Style.Cpp.Comment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            txtOutput.Styles[Style.Cpp.CommentLine].ForeColor = Color.FromArgb(0, 128, 0); // Green
            txtOutput.Styles[Style.Cpp.CommentLineDoc].ForeColor = Color.FromArgb(128, 128, 128); // Gray
            txtOutput.Styles[Style.Cpp.Number].ForeColor = Color.Olive;
            txtOutput.Styles[Style.Cpp.Word].ForeColor = Color.Blue;
            txtOutput.Styles[Style.Cpp.Word2].ForeColor = Color.Blue;
            txtOutput.Styles[Style.Cpp.String].ForeColor = Color.FromArgb(163, 21, 21); // Red
            txtOutput.Styles[Style.Cpp.Character].ForeColor = Color.FromArgb(163, 21, 21); // Red
            txtOutput.Styles[Style.Cpp.Verbatim].ForeColor = Color.FromArgb(163, 21, 21); // Red
            txtOutput.Styles[Style.Cpp.StringEol].BackColor = Color.Pink;
            txtOutput.Styles[Style.Cpp.Operator].ForeColor = Color.Purple;
            txtOutput.Styles[Style.Cpp.Preprocessor].ForeColor = Color.Maroon;
        }
    }
}