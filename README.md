# Demo project

## MyBlazorApp

This project shows an error when using the CascadeValue property and the SetParametersAsync initialization method of child components.

If the CascadeValue is a complex object and is passed to its children with `IsFixed=false`, 
then the `SetParametersAsync` method is called twice. The first call contains stale parameter values, which leads 
to subsequent errors when components bind to properties marked with `[Parameter]` or `[Parameter, SupplyParameterFromQuery]`.