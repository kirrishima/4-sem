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

// Задание 2
interface Car {
    manufacturer: string;
    model: string;
}

type CarsType = Partial<Car>;

let car: CarsType = {};
car.manufacturer = "manufacturer";
car.model = 'model';

// Задание 3
interface ArrayCarsType {
    cars: Car[];
}

let car1: CarsType = {};
car1.manufacturer = "manufacturer";
car1.model = 'model';

let car2: CarsType = {};
car2.manufacturer = "manufacturer";
car2.model = 'model';

let arrayCars: Array<ArrayCarsType> = [{
    cars: [car1 as Car, car2 as Car]
}];

// Задание 4
type MarkFilterType = 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10;

type DoneType = boolean;

type GroupFilterType = 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 | 12;

interface MarkType {
    subject: string;
    mark: MarkFilterType;
    done: DoneType;
}

interface StudentType {
    id: number;
    name: string;
    group: GroupFilterType;
    marks: MarkType[];
}

interface GroupType {
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
