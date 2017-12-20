using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2017.Solvers
{
    internal class Day20Solver : IProblemSolver
    {
        public static IProblemSolver Create() => new Day20Solver();

        public void Solve(string fileText)
        {
            SolvePart1(fileText);
            SolvePart2(fileText);
        }

        private void SolvePart1(string fileText)
        {
            var particles = fileText.SplitIntoLines()
                .Select(ParseParticle);
            var bestParticle = particles.First();
            var answer = 0;
            var current = 1;
            foreach (var particle in particles.Skip(1))
            {
                if (ParticleIsCloserToOriginInLongTerm(particle, bestParticle))
                {
                    bestParticle = particle;
                    answer = current;
                }
                current++;
            }
            Output.Answer(answer);
        }

        private void SolvePart2(string fileText)
        {
            var particles = fileText.SplitIntoLines()
                .Select(ParseParticle)
                .ToList();
            while (FutureCollisionIsPossible(particles))
            {
                particles.ForEach(p => p.SimulateStep());
                RemoveCollidingParticles(particles);
            }
            Output.Answer(particles.Count);
        }

        private bool FutureCollisionIsPossible(List<Particle> particles)
        {
            var orderedParticles = particles.OrderBy(p => p.CurrentDistance()).ToList();
            var possibleCollisionInFuture = false;
            for (var i = 0; i < orderedParticles.Count - 1; i++)
            {
                if (ParticleIsCloserToOriginInLongTerm(orderedParticles[i + 1], orderedParticles[i]))
                    possibleCollisionInFuture = true;
                ;
            }
            return possibleCollisionInFuture;
        }

        private static void RemoveCollidingParticles(List<Particle> particles)
        {
            var collidingGroups =
                particles.GroupBy(p => new { X = p.Position[0], Y = p.Position[1], Z = p.Position[2] }, p => p);
            foreach (var group in collidingGroups.Where(g => g.Count() > 1))
            {
                particles.RemoveAll(p => @group.Contains(p));
            }
        }

        private bool ParticleIsCloserToOriginInLongTerm(Particle particle, Particle bestParticle)
        {
            if (particle.AccelerationMagnitude() < bestParticle.AccelerationMagnitude()) return true;

            if (particle.AccelerationMagnitude() == bestParticle.AccelerationMagnitude()
                && particle.VelocityPlusAccelerationMagnitude() < bestParticle.VelocityPlusAccelerationMagnitude())
                return true;

            if (particle.AccelerationMagnitude() == bestParticle.AccelerationMagnitude()
                && particle.VelocityPlusAccelerationMagnitude() == bestParticle.VelocityPlusAccelerationMagnitude()
                && particle.AllMagnitude() < bestParticle.AllMagnitude())
                return true;

            return false;
        }

        private Particle ParseParticle(string arg)
        {
            var pattern = "p=<(.+)>, v=<(.+)>, a=<(.+)>";
            var match = Regex.Match(arg, pattern);

            return new Particle()
            {
                Position = match.Groups[1].Value.Split(',').Select(int.Parse).ToArray(),
                Velocity = match.Groups[2].Value.Split(',').Select(int.Parse).ToArray(),
                Acceleration = match.Groups[3].Value.Split(',').Select(int.Parse).ToArray(),
            };
        }

        private class Particle
        {
            public int[] Position { get; set; }
            public int[] Velocity { get; set; }
            public int[] Acceleration { get; set; }

            public int AccelerationMagnitude()
            {
                return Math.Abs(Acceleration[0])
                    + Math.Abs(Acceleration[1])
                    + Math.Abs(Acceleration[2]);
            }

            public int VelocityPlusAccelerationMagnitude()
            {
                return Math.Abs(Acceleration[0] + Velocity[0])
                    + Math.Abs(Acceleration[1] + Velocity[1])
                    + Math.Abs(Acceleration[2] + Velocity[2]);
            }

            public int AllMagnitude()
            {
                return Math.Abs(Acceleration[0] + Velocity[0] + Position[0])
                    + Math.Abs(Acceleration[1] + Velocity[1] + Position[1])
                    + Math.Abs(Acceleration[2] + Velocity[2] + Position[2]);
            }

            public int CurrentDistance()
            {
                return Position.Select(Math.Abs).Sum();
            }

            public void SimulateStep()
            {
                for (var i = 0; i < 3; i++)
                {
                    Velocity[i] += Acceleration[i];
                    Position[i] += Velocity[i];
                }
            }
        }
    }
}