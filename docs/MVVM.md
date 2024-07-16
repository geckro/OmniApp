# MVVM (Model-View-ViewModel)

_MVVM_ is an architectural pattern that is used for this project. MVVM is separated into 3 parts, a Model, a View and ViewModel.

_**Note**: Business logic is just a fancy term for how data is processed and decisions to achieve the requirements for the app. It got its name by the rules of the business you're writing a program for, to explain it to non-technical folk._

## Model

The _Model_ is the application's data and the business logic. In this case, the `Game` class.

## View

The _View_ is the `XAML`/`.cs` for the window. It handles the UI and binds itself to the ViewModel to display data and respond to user interactions.

## ViewModel

The _ViewModel_ is practically a bridge between the Model and the View. It contains the business logic, commands and properties that the View binds to.
