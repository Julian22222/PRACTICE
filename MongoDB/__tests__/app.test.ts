const request = require("supertest");
const app = require("../server");
const db = require("../models/movie");

afterAll(() => {
  if (db.end) db.end();
});

describe("movie", () => {
  describe("get movie", () => {
    describe("given movie doesn't exist", () => {
      it("should return a 404", () => {
        const movieId = "999";

        return request(app)
          .get("/movies/${movieId}")
          .expect(404)
          .then((res) => {
            expect(res).toBe(false);
          });
      });
    });
  });
});
