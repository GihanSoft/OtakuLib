﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using OtakuLib.Logic.Components;
using OtakuLib.Logic.ViewModels;

namespace OtakuLib.WPF.Components;
/// <summary>
/// Interaction logic for WebtoonPagesViewer.xaml
/// </summary>
public partial class WebtoonPagesViewer : UserControl, IPagesViewer, INotifyPropertyChanged
{
    private GridLength haWidth;

    public WebtoonPagesViewer(IPagesViewerVM viewModel)
    {
        Title = "Webtoon Viewer";
        ViewModel = viewModel;
        HaWidth = new GridLength(200);
        InitializeComponent();
    }

    public IPagesViewerVM ViewModel { get; }

    public string Title { get; }

    public GridLength HaWidth
    {
        get => haWidth;
        set
        {
            haWidth = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HaWidth)));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}
