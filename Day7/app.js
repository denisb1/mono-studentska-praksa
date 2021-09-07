let input = document.getElementById('input');
let numbers = document.querySelectorAll('.numbers');
let operators = document.querySelectorAll('.operators');
let equal = document.getElementById('equal');
let	clear = document.getElementById('clear');
let dot = document.getElementById('dot');
let	displayed = false;

for (let i = 0; i < numbers.length; i++) {
	numbers[i].addEventListener("click", function(e) {
		let string = input.innerHTML;
		let char = string[string.length - 1];

		if (displayed === false) {
			input.innerHTML += e.target.innerHTML;
		} else if (displayed === true && char === "+" || char === "-" || char === "*" || char === "/") {
			displayed = false;
			input.innerHTML += e.target.innerHTML;
		} else {
			displayed = false;
			input.innerHTML = "";
			input.innerHTML += e.target.innerHTML;
		}
	});
}

for (let i = 0; i < operators.length; i++) {
	operators[i].addEventListener("click", function(e) {
		let string = input.innerHTML;
		let char = string[string.length - 1];

		if (char === "+" || char === "-" || char === "*" || char === "/") {
			input.innerHTML = string.substring(0, string.length - 1) + e.target.innerHTML;
		} else if (string.length === 0) {
			// ignore
		} else if (char === '-') {
			input.innerHTML += '-';
		} else {
			input.innerHTML += e.target.innerHTML;
		}
	});
}

dot.addEventListener("click", function (e) {
	let string = input.innerHTML;

	if (string && string.indexOf('.') < 1)
		input.innerHTML += e.target.innerHTML;

});

equal.addEventListener("click", function () {
	let string = input.innerHTML;
	let numbers = string.split(/\+|\-|\*|\//g);
	let operators = string.replace(/[0-9]|\./g, "").split("");

	let div = operators.indexOf("/");
	while (div !== -1) {
		numbers.splice(div, 2, numbers[div] / numbers[div + 1]);
		operators.splice(div, 1);
		div = operators.indexOf("/");
	}

	let mul = operators.indexOf("*");
	while (mul !== -1) {
		numbers.splice(mul, 2, numbers[mul] * numbers[mul + 1]);
		operators.splice(mul, 1);
		mul = operators.indexOf("*");
	}

	let add = operators.indexOf("+");
	while (add !== -1) {
		numbers.splice(add, 2, parseFloat(numbers[add]) + parseFloat(numbers[add + 1]));
		operators.splice(add, 1);
		add = operators.indexOf("+");
	}

	let sub = operators.indexOf("-");
	while (sub !== -1) {
		numbers.splice(sub, 2, numbers[sub] - numbers[sub + 1]);
		operators.splice(sub, 1);
		sub = operators.indexOf("-");
	}

	input.innerHTML = numbers[0];
	displayed = true;
});

clear.addEventListener("click", function() {
	input.innerHTML = "";
});
