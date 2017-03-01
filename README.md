# BowlingPointsCalculator
There are 3 project in the solution:

1) Bowling point - contains a calculator that generates the results when bowling points are given:
https://github.com/skat/bowling-opgave

2) Unit tests for the bowling calculator

3) A console application that gets the points from the webapi, calculates the results using the BowlingCalculator 
and then validates the calculated result based using the webapi

The results of the validation are displayed in a log file. 

The results are marked as wrong just for cases when the game is not complet but the last ball was a strike
or a spare. Here is an example:
[[4,1],[8,1],[4,4],[2,5],[1,9]] returns [5,14,22,29]. We don't know yet the points for the last frame 
([1,9]), one more ball should be thrown to calculated the bonus.
The failing result validations where verified in the simulator(http://www.bowlinggenius.com/) and
I was getting the same results.
