// Задание 1
interface Student {
    id: number;
    name: string;
    group: number;
}

let array: Student[] = [
    { id: 1, name: 'Vasya', group: 10 },
    { id: 2, name: 'Ivan', group: 11 },
    { id: 3, name: 'Masha', group: 12 },
    { id: 4, name: 'Petya', group: 10 },
    { id: 5, name: 'Kira', group: 11 },
];

console.log(array);

// Задание 2
interface CarsType {
    manufacturer?: string;
    model?: string;
}


let car: CarsType = {};
car.manufacturer = "manufacturer";
car.model = 'model';

console.log(car.manufacturer);
console.log(car.model);
// Задание 3
interface ArrayCarsType {
    cars: CarsType[];
}

let car1: CarsType = {};
car1.manufacturer = "manufacturer";
car1.model = 'model';

let car2: CarsType = {};
car2.manufacturer = "manufacturer";
car2.model = 'model';

let arrayCars: Array<ArrayCarsType> = [{
    cars: [car1 as CarsType, car2 as CarsType]
}];

// Задание 4
type MarkFilterType = 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10;

type DoneType = boolean;

type GroupFilterType = 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 | 12;

type MarkType = {
    subject: string;
    mark: MarkFilterType;
    done: DoneType;
}

type StudentType = {
    id: number;
    name: string;
    group: GroupFilterType;
    marks: MarkType[];
}

type GroupType = {
    students: StudentType[];
    studentsFilter: (group: GroupFilterType) => StudentType[];
    marksFilter: (mark: MarkFilterType) => StudentType[];
    deleteStudent: (id: number) => void;
    mark: MarkFilterType;
    group: GroupFilterType;
}

let group: GroupType = {
    students: [],
    studentsFilter(group) {
        return this.students.filter(student => student.group === group);
    },
    marksFilter(mark) {
        return this.students.filter(student =>
            student.marks.some(m => m.mark === mark)
        );
    },
    deleteStudent(id) {
        this.students = this.students.filter(student => student.id !== id);
    },
    mark: 5,
    group: 10,
};

group.students.push({
    id: 1,
    name: "Иван",
    group: 10,
    marks: [
        { subject: "Математика", mark: 5, done: true },
        { subject: "Физика", mark: 7, done: false }
    ]
});

group.students.push({
    id: 2,
    name: "Олег",
    group: 10,
    marks: [
        { subject: "Экономика", mark: 8, done: true },
        { subject: "Бизнес", mark: 6, done: true }
    ]
});

group.students.push({
    id: 3,
    name: "Сергей",
    group: 9,
    marks: [
        { subject: "ТПИ", mark: 5, done: true },
        { subject: "Математика", mark: 3, done: false }
    ]
});

console.log("Все студенты:", group.students);

const studentsGroup10 = group.studentsFilter(10);
console.log("Студенты группы 10:", studentsGroup10);

const studentsWithMark5 = group.marksFilter(5);
console.log("Студенты с оценкой 5:", studentsWithMark5);

group.deleteStudent(2);
console.log("Студенты после удаления студента с id = 2:", group.students);
