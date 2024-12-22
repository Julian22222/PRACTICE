const request = require("supertest"); //Import Supertest
const app = require("../app"); //Connection to all routes
const pool = require("../db"); //import DB connection
const seed = require("../seed");
const { describe } = require("node:test");

//this before each invoke seed function beforeEach describe block
//if you want to invoke seed before each test --> put beforeeach inside before each block

beforeEach(async () => await seed());

afterAll(() => {
  pool.end();
});

describe("General Testing FOR GET METHOD", () => {
  // describe("get car", () => {
  //   describe("given car doesn't exist", () => {
  //     it("should return a 404", () => {
  //       const carId = "999";

  //       return request(app)
  //         .get("/${carId}")
  //         .expect(404)
  //         .then((res) => {
  //           expect(res).toBe(false);
  //         });
  //     });
  //   });
  // });

  test("1. checking for StatusCode 200", async () => {
    const res = await request(app)
      .get("/")
      .expect("Content-Type", /json/)
      .expect(200);

    expect(res.statusCode).toEqual(200);
  });

  test("2. respond with all cars", async () => {
    await request(app)
      .get("/")
      .expect(200)
      .then(({ body }) => {
        expect(body).toEqual([
          {
            car_id: 1,
            brand: "Bugatti",
            seats: 2,
            year: "2023-10-02T00:00:00.000Z",
            fuel: "petrol",
          },
          {
            car_id: 2,
            brand: "Bentley",
            seats: 5,
            year: "2023-09-02T00:00:00.000Z",
            fuel: "hybrid",
          },
          {
            car_id: 3,
            brand: "Skoda",
            seats: 5,
            year: "2023-08-10T00:00:00.000Z",
            fuel: "electric",
          },
          {
            car_id: 4,
            brand: "Porsche",
            seats: 2,
            year: "2023-07-09T00:00:00.000Z",
            fuel: "petrol",
          },
          {
            car_id: 5,
            brand: "Volkswagen",
            seats: 5,
            year: "2022-05-11T00:00:00.000Z",
            fuel: "diesel",
          },
          {
            car_id: 6,
            brand: "Lamborghini",
            seats: 2,
            year: "2023-04-19T00:00:00.000Z",
            fuel: "electric",
          },
          {
            car_id: 7,
            brand: "Volvo",
            seats: 6,
            year: "2023-10-05T00:00:00.000Z",
            fuel: "hybrid",
          },
        ]);
      });
  });

  test("3. check for array", () => {
    return request(app)
      .get("/")
      .expect(200)
      .then(({ body }) => {
        expect(body).toHaveLength(7);
        expect(Array.isArray(body)).toBe(true);
      });
  });

  test("4. checking length array", async () => {
    await request(app)
      .get("/")
      .expect(200)
      .then(({ body }) => {
        expect(body).toHaveLength(7);
      });
  });

  test("5. Get object 4", async () => {
    await request(app)
      .get("/4")
      .expect(200)
      .then((response) => {
        // console.log(response);

        expect(response._body[0]).toEqual({
          car_id: 4,
          brand: "Porsche",
          seats: 2,
          year: "2023-07-09T00:00:00.000Z",
          fuel: "petrol",
        });
      });
  });
});

describe("Error handling with GET method", () => {
  it("6. stuts 400, handling error for 400", async () => {
    await request(app)
      .get("/tom")
      .expect(400)
      .then((body) => {
        // console.log("BODY", body);
        expect(body.error.text).toBe("Wrong card Id has been inserted.");
      });
  });

  it("stuts 404, handling error for 999", async () => {
    await request(app)
      .get("/999")
      .expect(404)
      .then((body) => {
        // console.log(body);
        expect(body.error.text).toBe("Car Id Not Found");
      });
  });

  it("data type check", async () => {
    await request(app)
      .get("/")
      .expect(200)
      .then(({ body }) => {
        // console.log(body);

        expect(typeof body).toBe("object");

        body.forEach((eachCar) => {
          expect(eachCar).toEqual(
            expect.objectContaining({
              car_id: expect.any(Number),
              brand: expect.any(String),
              seats: expect.any(Number),
              year: expect.any(String),
              fuel: expect.any(String),
            })
          );
        });
      });
  });

  describe("2. POST METHOD TESTS ", () => {
    test(" checking for StatusCode 201 when POST", async () => {
      const newCar = {
        brand: "Audi",
        seats: 5,
        year: "2018-11-02T00:00:00.000Z",
        fuel: "petrol",
      };
      await request(app)
        .post("/")
        // .set("Content-Type", "application/json")
        .send(newCar)
        .expect(201)
        .then((response) => {
          // console.log("HEREEE", response);
          expect(response.statusCode).toEqual(201);
        });
    });

    test(" checking for Wrong Brand Input", async () => {
      const newCar = {
        seats: 5,
        year: "2012-11-02T00:00:00.000Z",
        fuel: "petrol",
      };
      await request(app)
        .post("/")
        // .set("Content-Type", "application/json")
        .send(newCar)
        .expect(400)
        .then((response) => {
          // console.log("HEREEE", response);
          expect(response.error.text).toBe("Wrong Brand Input");
        });
    });
  });

  test(" checking for object content", async () => {
    const newCar = {
      brand: "Audi",
      seats: 5,
      year: "2018-11-02T00:00:00.000Z",
      fuel: "petrol",
    };
    await request(app)
      .post("/")
      .send(newCar)
      .set("Content-Type", "application/json")
      .expect(201)
      .then((response) => {
        // console.log("RESPONSE", response);
        expect(response.request._data.brand).toBe("Audi");
        expect(response.request._data.seats).toBe(5);
        expect(response.request._data.year).toBe("2018-11-02T00:00:00.000Z");
        expect(response.request._data.fuel).toBe("petrol");
        expect(response.text).toBe("Data Inserted Successfully");
      });
  });
});
////

describe("Put method", () => {
  it("change brand", async () => {
    const newCar = {
      brand: "Toyota",
      seats: 5,
      year: "2012-11-02T00:00:00.000Z",
      fuel: "petrol",
    };
    await request(app)
      .put("/3")
      // .set("Content-Type", "application/json")
      .send(newCar)
      .expect(204)
      .then((response) => {
        // console.log("HEREEE", response);
        // expect(response.text).toBe("Data Updated Successfully");
        expect(response.statusCode).toBe(204);
        expect(response.request._data).toEqual({
          brand: "Toyota",
          seats: 5,
          year: "2012-11-02T00:00:00.000Z",
          fuel: "petrol",
        });
      });
  });
});

describe("Delete method", () => {
  it("Delete /7,  Volvo delete. status 204", async () => {
    await request(app)
      .delete("/7")
      .expect(204)
      .then((res) => {
        // console.log("HEREEE TEST", res);

        expect(res.body).toEqual({});
      });
  });

  test("Delete /999, status 400", async () => {
    await request(app)
      .delete("/999")
      .expect(404)
      .then((response) => {
        console.log(response);

        expect(response.error.text).toEqual("Car Id not found");
      });
  });
});
