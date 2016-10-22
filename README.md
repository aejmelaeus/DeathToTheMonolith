# DeathToTheMonolith

This is a project that was used during my talk at SweNug (https://www.meetup.com/Swenug-Stockholm/events/233758805/)

## What it does

It is a simple application mimicking some kind of order processor in a e-commerse application. There is a flow that starts with the command `PlaceOrder`. The shipping can be made only after the billing is completed. When all steps are done a `OrderCompleted` event is published.

## Test it yourself

There are a few things you need to install:
* Install SEQ (https://getseq.net/)
* Install RavenDb (https://ravendb.net/downloads)

To run it:
* Open the project in Visual Studio
* Configure the Solution to run all executables (Right click the solution file => Properties => Common Properties => Startup Project => Multiple startup projects: `Billing` + `Order` + `Shipping` + `UI`)
* Press F5
* Observe the flow in SEQ (http://localhost:5341/)