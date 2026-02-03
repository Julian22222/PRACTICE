var numberValue = 42;
console.log(numberValue);
var str = "Hello, TypeScript!";
console.log(str);
var isActive = true;
console.log(isActive);
var arr = [1, 2, 3, 4, 5];
console.log(arr);
var tuple = [1, "TypeScript"];
console.log(tuple);
var anyValue = "This can be anything";
console.log(anyValue);
function greet(name) {
    return "Hello, ".concat(name, "!");
}
console.log(greet("World"));
function displayPerson(person) {
    console.log("Name: ".concat(person.name, ", Age: ").concat(person.age));
}
var person = { name: "Alice", age: 30 };
displayPerson(person);
var Color;
(function (Color) {
    Color[Color["Red"] = 0] = "Red";
    Color[Color["Green"] = 1] = "Green";
    Color[Color["Blue"] = 2] = "Blue";
})(Color || (Color = {}));
var myColor = Color.Green;
console.log(myColor); // Output: 1 (index of Green in the enum)
var Animal = /** @class */ (function () {
    function Animal(name) {
        this.name = name;
    }
    Animal.prototype.speak = function () {
        console.log("".concat(this.name, " makes a noise."));
    };
    return Animal;
}());
