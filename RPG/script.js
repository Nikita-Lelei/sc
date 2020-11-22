function getRandom() {
    return Math.floor(Math.random() * 100);
}
function attackOrBlock() {
    var attackChance = getRandom();
    var blockChance = getRandom();
    if (attackChance < blockChance) {
        return 1;
    }
    return 0;
}
class Human {
    constructor(HP, damage, defense, classAttribute) {
        if (HP > 200) {
            HP = 200;
        }
        if (classAttribute > 10) {
            classAttribute = 10;
        }
        this.name = "";
        this.HP = HP;
        this.damage = damage;
        this.defense = defense;
        this.classAttribute = classAttribute;
    }


    getDamage(dmg) {
        this.HP -= dmg;
        if(this.HP < 0) {
            this.HP = 0;
        }
    }
    block(character) {
        if (character.damage <= this.defense * 1.5) {
            console.log(`${this.name} blocked 100% of damage`);
        } else {
            this.getDamage(character.damage - (this.defense * 1.5));
        }

    }

    attack(character) {
        character.getDamage(this.damage - character.defense)
    }

    isDead() {
        if (this.HP <= 0) {
            console.log(`${this.name} is dead`)
            return true;
        }
        return false;
    }
}

class Archer extends Human {
    name = "Archer";
    getDamage(dmg) {
        var rnd = getRandom();
        var dodgeChance = this.dodge()
        if (rnd > dodgeChance) {
            this.HP -= dmg;
            if(this.HP < 0) {
                this.HP = 0;
            }
        } else {
            console.log(`${this.name} dodge`);
        }
    }
    dodge() { //чем больше аттрибут тем больше шанс уклонения
        var dodgeChance = this.classAttribute * 7;
        return dodgeChance;
    }
}

class Warrior extends Human {
    name = "Warrior";
    attack(character) {
        var rnd = getRandom();
        var critChance = this.crit()
        if (rnd > critChance) {
            character.getDamage(this.damage - character.defense);
        } else {
            console.log(`${this.name} crit!`);
            character.getDamage(this.damage * 1.4 - character.defense);

        }
    }
    crit() { //чем больше аттрибут тем больше шанс крита
        var critChance = this.classAttribute * 5;
        return critChance;
    }
}

class Mage extends Human {
    name = "Mage";
    maxHP = this.HP;

    attack(character) {
        var rnd = getRandom();
        var stealChance = this.lifeSteal()
        if (rnd <= stealChance) {
            if (this.HP < this.maxHP) {
                this.HP += (this.damage - character.defense) / 2;
                console.log(`${this.name} healed 50% damage`)
                this.HP > this.maxHP ? this.HP = this.maxHP : 0;
            }
        }
        character.getDamage(this.damage - character.defense)

    }

    lifeSteal() { //больше аттрибут - больше шанс лайфстила
        var stealChance = this.classAttribute * 8;
        return stealChance;
    }
}

async function fight(entityFirst, entitySecond) {
    return new Promise((resolve, reject) => {

        setTimeout(() => {
            var first = attackOrBlock();
            var second = attackOrBlock();
            switch (first < second ? -1 : (first > second ? 1 : 0)) {
                case 0: {
                    if (first == 1) { //у двоих атака
                        console.log("attack both");
                        entityFirst.attack(entitySecond);
                        entitySecond.attack(entityFirst);
                    } else { //у двоих блок
                        console.log("2 blocks!")
                    }
                }; break;
                //если у первого атака у второго блок
                case 1: entitySecond.block(entityFirst); console.log(`attack ${entityFirst.name} / ${entitySecond.name} block`); break;
                //если у первого болк у второго атака
                case -1: entityFirst.block(entitySecond); console.log(`attack ${entitySecond.name} / ${entityFirst.name} block`); break;
            }
            resolve();
        }, 2000);
    })

}
async function battle(first, second) {
    while (!first.isDead() && !second.isDead()) {
        await fight(first, second);
        console.log(`HP ${first.name} = ${first.HP} HP ${second.name} = ${second.HP}`);
        console.log("________________________________________________")
    }
}


//ХП, УРОН, ЗАЩИТА, УНИКАЛЬНЫЙ АТТРИБУТ ПЕРСОНАЖА
// максимальное ХП 200 (если больше будет установлено 200), урон и защита любая, аттрибут до 10 (если больше будет установлено 10)

var warrior = new Warrior(200, 40, 30, 5);
var archer = new Archer(100, 65, 10, 7);
var mage = new Mage(70, 80, 5, 9)

battle(warrior, archer);



