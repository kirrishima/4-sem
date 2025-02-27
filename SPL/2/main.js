var array = [
    { id: 1, name: 'Vasya', group: 10 },
    { id: 2, name: 'Ivan', group: 11 },
    { id: 3, name: 'Masha', group: 12 },
    { id: 4, name: 'Petya', group: 10 },
    { id: 5, name: 'Kira', group: 11 },
];
console.log(array);
var car = {};
car.manufacturer = "manufacturer";
car.model = 'model';
console.log(car.manufacturer);
console.log(car.model);
var car1 = {};
car1.manufacturer = "manufacturer";
car1.model = 'model';
var car2 = {};
car2.manufacturer = "manufacturer";
car2.model = 'model';
var arrayCars = [{
        cars: [car1, car2]
    }];
var group = {
    students: [],
    studentsFilter: function (group) {
        return this.students.filter(function (student) { return student.group === group; });
    },
    marksFilter: function (mark) {
        return this.students.filter(function (student) {
            return student.marks.some(function (m) { return m.mark === mark; });
        });
    },
    deleteStudent: function (id) {
        this.students = this.students.filter(function (student) { return student.id !== id; });
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
var studentsGroup10 = group.studentsFilter(10);
console.log("Студенты группы 10:", studentsGroup10);
var studentsWithMark5 = group.marksFilter(5);
console.log("Студенты с оценкой 5:", studentsWithMark5);
group.deleteStudent(2);
console.log("Студенты после удаления студента с id = 2:", group.students);
