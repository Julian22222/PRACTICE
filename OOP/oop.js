class Animal{
    constructor(name, type){
        this._name = name;
        this._type = type;
        this._home = true;
    }

    //method --> function
    eat(){
        return `${this._name} is ${this._type} type animal`
    }
}

const cat = new Animal("Tom", "home");

// console.log(cat)

// console.log(cat.eat())


class Human extends Animal{
    constructor(_name,_type,work, hobby){
        super(_name,_type);
        this.work =work;
        this.hobby = hobby;
    }

    eat(){
        return `${this._name} likes ${this.hobby}`
    }
}

const jon = new Human("Jon","any","It", "footbal")

// console.log(jon._type)
// console.log(jon.eat())

class Temperature {
    constructor(temp, wind_speed){
        this._temp = temp;
        this._wind_speed = wind_speed;
    }

get celsius(){
    return `${this._temp}`
}

set farenhait(myTemp){
    this._temp=myTemp
}

farenhait(){
    return `${this.temp * 2} is showen in farenhait and there is ${this.wind_speed}`
}

}

const uk = new Temperature(12, 5)

console.log(uk.celsius)

uk.farenhait = 40;
console.log(uk.farenhait)