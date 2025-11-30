var years = Enumerable.Range(2015, 11);
var days = Enumerable.Range(1, 25);

var problems = years.SelectMany(year => days.Select(day => (year, day)))
	.Where(problem => AdventOfCode.Input.Exists(problem.year, problem.day))
	.ToArray();

var (year, day) = problems.Last();

AdventOfCode.Problem.Solve(year, day);