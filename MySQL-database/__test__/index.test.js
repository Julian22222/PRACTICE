const request = require("supertest"); //Import Supertest
const app = require("../app"); //Connection to all routes
const pool = require("../db"); //import DB connection

afterAll(() => {
  if (pool.end) pool.end();
});

describe("1. GET METHOD TESTS /", () => {
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

  test(" checking for StatusCode 200", async () => {
    const res = await request(app)
      .get("/")
      .expect("Content-Type", /json/)
      .expect(200);

    expect(res.statusCode).toEqual(200);
  });

  test.only("200: respond with all cars", async () => {
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
            fuel: "disel",
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

  test("check for array", () => {
    return request(app)
      .get("/")
      .expect(200)
      .then(({ body }) => {
        expect(body).toHaveLength(7);
        expect(Array.isArray(body)).toBe(true);
      });
  });

  test("checking length array", async () => {
    await request(app)
      .get("/")
      .expect(200)
      .then(({ body }) => {
        expect(body).toHaveLength(7);
      });
  });

  it("stuts 404, handling error", async () => {
    await request(app)
      .get("/tom")
      .expect(404)
      .then(({ body }) => {
        console.log("BODY", body);
        expect(body).toBe("Wrong card Id has been inserted");
      });
  });

  it("data type check", async () => {
    await request(app)
      .get("/")
      .expect(200)
      .then(({ body }) => {
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

  //////
});
