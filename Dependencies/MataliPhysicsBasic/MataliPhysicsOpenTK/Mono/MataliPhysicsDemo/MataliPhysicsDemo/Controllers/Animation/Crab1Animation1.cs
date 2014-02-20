/*
    Matali Physics Demo
    Copyright (c) 2013 KOMIRES Sp. z o. o.
 */
using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Komires.MataliPhysics;

namespace MataliPhysicsDemo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Crab1Animation1
    {
        Demo demo;
        PhysicsScene scene;
        string instanceIndexName;

        Constraint rightLimbFront1Constraint;
        Constraint rightLimbFront2Constraint;
        Constraint rightLimbFront3Constraint;
        Constraint rightLimbMiddleFront1Constraint;
        Constraint rightLimbMiddleFront2Constraint;
        Constraint rightLimbMiddleFront3Constraint;
        Constraint rightLimbMiddleBack1Constraint;
        Constraint rightLimbMiddleBack2Constraint;
        Constraint rightLimbMiddleBack3Constraint;
        Constraint rightLimbBack1Constraint;
        Constraint rightLimbBack2Constraint;
        Constraint rightLimbBack3Constraint;
        Constraint leftLimbFront1Constraint;
        Constraint leftLimbFront2Constraint;
        Constraint leftLimbFront3Constraint;
        Constraint leftLimbMiddleFront1Constraint;
        Constraint leftLimbMiddleFront2Constraint;
        Constraint leftLimbMiddleFront3Constraint;
        Constraint leftLimbMiddleBack1Constraint;
        Constraint leftLimbMiddleBack2Constraint;
        Constraint leftLimbMiddleBack3Constraint;
        Constraint leftLimbBack1Constraint;
        Constraint leftLimbBack2Constraint;
        Constraint leftLimbBack3Constraint;

        Vector3 scale;

        bool moveUp;

        float maxUp1AngleX;
        float maxUp2AngleX;
        float maxUp3AngleX;

        float maxUpFrontAngleY;
        float maxUpMiddleFrontAngleY;
        float maxUpMiddleBackAngleY;
        float maxUpBackAngleY;

        float maxAhead1AngleY;
        float maxAhead2AngleY;
        float maxAhead3AngleY;
        float maxAhead4AngleY;
        float maxAhead5AngleY;

        float rightFront1AngleYStep;
        float rightFront1AngleXStep;
        float rightFront2AngleXStep;
        float rightFront3AngleXStep;

        float rightMiddleFront1AngleYStep;
        float rightMiddleFront1AngleXStep;
        float rightMiddleFront2AngleXStep;
        float rightMiddleFront3AngleXStep;

        float rightMiddleBack1AngleYStep;
        float rightMiddleBack1AngleXStep;
        float rightMiddleBack2AngleXStep;
        float rightMiddleBack3AngleXStep;

        float rightBack1AngleYStep;
        float rightBack1AngleXStep;
        float rightBack2AngleXStep;
        float rightBack3AngleXStep;

        float leftFront1AngleYStep;
        float leftFront1AngleXStep;
        float leftFront2AngleXStep;
        float leftFront3AngleXStep;

        float leftMiddleFront1AngleYStep;
        float leftMiddleFront1AngleXStep;
        float leftMiddleFront2AngleXStep;
        float leftMiddleFront3AngleXStep;

        float leftMiddleBack1AngleYStep;
        float leftMiddleBack1AngleXStep;
        float leftMiddleBack2AngleXStep;
        float leftMiddleBack3AngleXStep;

        float leftBack1AngleYStep;
        float leftBack1AngleXStep;
        float leftBack2AngleXStep;
        float leftBack3AngleXStep;

        public Crab1Animation1(Demo demo, int instanceIndex)
        {
            this.demo = demo;
            instanceIndexName = " " + instanceIndex.ToString();
        }

        public void Initialize(PhysicsScene scene)
        {
            this.scene = scene;
        }

        public void SetControllers()
        {
            moveUp = true;

            maxUp1AngleX = 10.0f;
            maxUp2AngleX = 30.0f;
            maxUp3AngleX = 30.0f;

            maxUpFrontAngleY = 60.0f;
            maxUpMiddleFrontAngleY = 20.0f;
            maxUpMiddleBackAngleY = -20.0f;
            maxUpBackAngleY = -40.0f;

            maxAhead1AngleY = 60.0f;
            maxAhead2AngleY = 20.0f;
            maxAhead3AngleY = 0.0f;
            maxAhead4AngleY = -20.0f;
            maxAhead5AngleY = -40.0f;

            rightFront1AngleYStep = 1.0f;
            rightFront1AngleXStep = 1.0f;
            rightFront2AngleXStep = 1.0f;
            rightFront3AngleXStep = 1.0f;

            rightMiddleFront1AngleYStep = 1.0f;
            rightMiddleFront1AngleXStep = 1.0f;
            rightMiddleFront2AngleXStep = 1.0f;
            rightMiddleFront3AngleXStep = 1.0f;

            rightMiddleBack1AngleYStep = 1.0f;
            rightMiddleBack1AngleXStep = 1.0f;
            rightMiddleBack2AngleXStep = 1.0f;
            rightMiddleBack3AngleXStep = 1.0f;

            rightBack1AngleYStep = 1.0f;
            rightBack1AngleXStep = 1.0f;
            rightBack2AngleXStep = 1.0f;
            rightBack3AngleXStep = 1.0f;

            leftFront1AngleYStep = -1.0f;
            leftFront1AngleXStep = -1.0f;
            leftFront2AngleXStep = -1.0f;
            leftFront3AngleXStep = -1.0f;

            leftMiddleFront1AngleYStep = -1.0f;
            leftMiddleFront1AngleXStep = -1.0f;
            leftMiddleFront2AngleXStep = -1.0f;
            leftMiddleFront3AngleXStep = -1.0f;

            leftMiddleBack1AngleYStep = -1.0f;
            leftMiddleBack1AngleXStep = -1.0f;
            leftMiddleBack2AngleXStep = -1.0f;
            leftMiddleBack3AngleXStep = -1.0f;

            leftBack1AngleYStep = -1.0f;
            leftBack1AngleXStep = -1.0f;
            leftBack2AngleXStep = -1.0f;
            leftBack3AngleXStep = -1.0f;

            rightLimbFront1Constraint = scene.Factory.ConstraintManager.Find("Crab 1 Right Limb Front 1 Constraint" + instanceIndexName);
            rightLimbFront2Constraint = scene.Factory.ConstraintManager.Find("Crab 1 Right Limb Front 2 Constraint" + instanceIndexName);
            rightLimbFront3Constraint = scene.Factory.ConstraintManager.Find("Crab 1 Right Limb Front 3 Constraint" + instanceIndexName);
            rightLimbMiddleFront1Constraint = scene.Factory.ConstraintManager.Find("Crab 1 Right Limb Middle Front 1 Constraint" + instanceIndexName);
            rightLimbMiddleFront2Constraint = scene.Factory.ConstraintManager.Find("Crab 1 Right Limb Middle Front 2 Constraint" + instanceIndexName);
            rightLimbMiddleFront3Constraint = scene.Factory.ConstraintManager.Find("Crab 1 Right Limb Middle Front 3 Constraint" + instanceIndexName);
            rightLimbMiddleBack1Constraint = scene.Factory.ConstraintManager.Find("Crab 1 Right Limb Middle Back 1 Constraint" + instanceIndexName);
            rightLimbMiddleBack2Constraint = scene.Factory.ConstraintManager.Find("Crab 1 Right Limb Middle Back 2 Constraint" + instanceIndexName);
            rightLimbMiddleBack3Constraint = scene.Factory.ConstraintManager.Find("Crab 1 Right Limb Middle Back 3 Constraint" + instanceIndexName);
            rightLimbBack1Constraint = scene.Factory.ConstraintManager.Find("Crab 1 Right Limb Back 1 Constraint" + instanceIndexName);
            rightLimbBack2Constraint = scene.Factory.ConstraintManager.Find("Crab 1 Right Limb Back 2 Constraint" + instanceIndexName);
            rightLimbBack3Constraint = scene.Factory.ConstraintManager.Find("Crab 1 Right Limb Back 3 Constraint" + instanceIndexName);
            leftLimbFront1Constraint = scene.Factory.ConstraintManager.Find("Crab 1 Left Limb Front 1 Constraint" + instanceIndexName);
            leftLimbFront2Constraint = scene.Factory.ConstraintManager.Find("Crab 1 Left Limb Front 2 Constraint" + instanceIndexName);
            leftLimbFront3Constraint = scene.Factory.ConstraintManager.Find("Crab 1 Left Limb Front 3 Constraint" + instanceIndexName);
            leftLimbMiddleFront1Constraint = scene.Factory.ConstraintManager.Find("Crab 1 Left Limb Middle Front 1 Constraint" + instanceIndexName);
            leftLimbMiddleFront2Constraint = scene.Factory.ConstraintManager.Find("Crab 1 Left Limb Middle Front 2 Constraint" + instanceIndexName);
            leftLimbMiddleFront3Constraint = scene.Factory.ConstraintManager.Find("Crab 1 Left Limb Middle Front 3 Constraint" + instanceIndexName);
            leftLimbMiddleBack1Constraint = scene.Factory.ConstraintManager.Find("Crab 1 Left Limb Middle Back 1 Constraint" + instanceIndexName);
            leftLimbMiddleBack2Constraint = scene.Factory.ConstraintManager.Find("Crab 1 Left Limb Middle Back 2 Constraint" + instanceIndexName);
            leftLimbMiddleBack3Constraint = scene.Factory.ConstraintManager.Find("Crab 1 Left Limb Middle Back 3 Constraint" + instanceIndexName);
            leftLimbBack1Constraint = scene.Factory.ConstraintManager.Find("Crab 1 Left Limb Back 1 Constraint" + instanceIndexName);
            leftLimbBack2Constraint = scene.Factory.ConstraintManager.Find("Crab 1 Left Limb Back 2 Constraint" + instanceIndexName);
            leftLimbBack3Constraint = scene.Factory.ConstraintManager.Find("Crab 1 Left Limb Back 3 Constraint" + instanceIndexName);

            PhysicsObject objectBase = null;

            objectBase = scene.Factory.PhysicsObjectManager.Find("Crab 1" + instanceIndexName);
            if (objectBase != null)
                objectBase.UserControllers.PostTransformMethods += new SimulateMethod(Go);
        }

        void Go(SimulateMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);
            
            objectBase.MainLocalTransform.GetScale(ref scale);

            if (((scene.SimulationFrameCount % 200) == 0) && (scale.Y < 4.0f))
            {
                Vector3.Multiply(ref scale, 1.1f, out scale);

                objectBase.MainLocalTransform.SetScale(ref scale);

                objectBase.UpdateFromMainLocalTransform();
            }

            if (moveUp)
            {
                GoUp(args);
            }
            else
                GoAhead(args);
        }

        void GoUp(SimulateMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            float time = args.Time;
            bool move = false;

            if (Math.Abs(rightLimbFront1Constraint.ControlDegAngleY - maxUpFrontAngleY) > Math.Abs(rightFront1AngleYStep))
            {
                move = true;
                if (rightLimbFront1Constraint.ControlDegAngleY < maxUpFrontAngleY)
                    rightLimbFront1Constraint.ControlDegAngleY += rightFront1AngleYStep;
                else
                    if (rightLimbFront1Constraint.ControlDegAngleY > maxUpFrontAngleY)
                        rightLimbFront1Constraint.ControlDegAngleY -= rightFront1AngleYStep;
            }

            if (Math.Abs(rightLimbMiddleFront1Constraint.ControlDegAngleY - maxUpMiddleFrontAngleY) > Math.Abs(rightMiddleFront1AngleYStep))
            {
                move = true;
                if (rightLimbMiddleFront1Constraint.ControlDegAngleY < maxUpMiddleFrontAngleY)
                    rightLimbMiddleFront1Constraint.ControlDegAngleY += rightMiddleFront1AngleYStep;
                else
                    if (rightLimbMiddleFront1Constraint.ControlDegAngleY > maxUpMiddleFrontAngleY)
                        rightLimbMiddleFront1Constraint.ControlDegAngleY -= rightMiddleFront1AngleYStep;
            }

            if (Math.Abs(rightLimbMiddleBack1Constraint.ControlDegAngleY - maxUpMiddleBackAngleY) > Math.Abs(rightMiddleBack1AngleYStep))
            {
                move = true;
                if (rightLimbMiddleBack1Constraint.ControlDegAngleY < maxUpMiddleBackAngleY)
                    rightLimbMiddleBack1Constraint.ControlDegAngleY += rightMiddleBack1AngleYStep;
                else
                    if (rightLimbMiddleBack1Constraint.ControlDegAngleY > maxUpMiddleBackAngleY)
                        rightLimbMiddleBack1Constraint.ControlDegAngleY -= rightMiddleBack1AngleYStep;
            }

            if (Math.Abs(rightLimbBack1Constraint.ControlDegAngleY - maxUpBackAngleY) > Math.Abs(rightMiddleBack1AngleYStep))
            {
                move = true;
                if (rightLimbBack1Constraint.ControlDegAngleY < maxUpBackAngleY)
                    rightLimbBack1Constraint.ControlDegAngleY += rightBack1AngleYStep;
                else
                    if (rightLimbBack1Constraint.ControlDegAngleY > maxUpBackAngleY)
                        rightLimbBack1Constraint.ControlDegAngleY -= rightBack1AngleYStep;
            }



            if (Math.Abs(leftLimbFront1Constraint.ControlDegAngleY + maxUpFrontAngleY) > Math.Abs(leftFront1AngleYStep))
            {
                move = true;
                if (leftLimbFront1Constraint.ControlDegAngleY > -maxUpFrontAngleY)
                    leftLimbFront1Constraint.ControlDegAngleY += leftFront1AngleYStep;
                else
                    if (leftLimbFront1Constraint.ControlDegAngleY < -maxUpFrontAngleY)
                        leftLimbFront1Constraint.ControlDegAngleY -= leftFront1AngleYStep;
            }

            if (Math.Abs(leftLimbMiddleFront1Constraint.ControlDegAngleY + maxUpMiddleFrontAngleY) > Math.Abs(leftMiddleFront1AngleYStep))
            {
                move = true;
                if (leftLimbMiddleFront1Constraint.ControlDegAngleY > -maxUpMiddleFrontAngleY)
                    leftLimbMiddleFront1Constraint.ControlDegAngleY += leftMiddleFront1AngleYStep;
                else
                    if (leftLimbMiddleFront1Constraint.ControlDegAngleY < -maxUpMiddleFrontAngleY)
                        leftLimbMiddleFront1Constraint.ControlDegAngleY -= leftMiddleFront1AngleYStep;
            }

            if (Math.Abs(leftLimbMiddleBack1Constraint.ControlDegAngleY + maxUpMiddleBackAngleY) > Math.Abs(leftMiddleBack1AngleYStep))
            {
                move = true;
                if (leftLimbMiddleBack1Constraint.ControlDegAngleY > -maxUpMiddleBackAngleY)
                    leftLimbMiddleBack1Constraint.ControlDegAngleY += leftMiddleBack1AngleYStep;
                else
                    if (leftLimbMiddleBack1Constraint.ControlDegAngleY < -maxUpMiddleBackAngleY)
                        leftLimbMiddleBack1Constraint.ControlDegAngleY -= leftMiddleBack1AngleYStep;
            }

            if (Math.Abs(leftLimbBack1Constraint.ControlDegAngleY + maxUpBackAngleY) > Math.Abs(leftBack1AngleYStep))
            {
                move = true;
                if (leftLimbBack1Constraint.ControlDegAngleY > -maxUpBackAngleY)
                    leftLimbBack1Constraint.ControlDegAngleY += leftBack1AngleYStep;
                else
                    if (leftLimbBack1Constraint.ControlDegAngleY < -maxUpBackAngleY)
                        leftLimbBack1Constraint.ControlDegAngleY -= leftBack1AngleYStep;
            }



            if (Math.Abs(rightLimbFront1Constraint.ControlDegAngleX - maxUp1AngleX) > Math.Abs(rightFront1AngleXStep))
            {
                move = true;
                if (rightLimbFront1Constraint.ControlDegAngleX < maxUp1AngleX)
                    rightLimbFront1Constraint.ControlDegAngleX += rightFront1AngleXStep;
                else
                    if (rightLimbFront1Constraint.ControlDegAngleX > maxUp1AngleX)
                        rightLimbFront1Constraint.ControlDegAngleX -= rightFront1AngleXStep;
            }

            if (Math.Abs(rightLimbFront2Constraint.ControlDegAngleX - maxUp2AngleX) > Math.Abs(rightFront2AngleXStep))
            {
                move = true;
                if (rightLimbFront2Constraint.ControlDegAngleX < maxUp2AngleX)
                    rightLimbFront2Constraint.ControlDegAngleX += rightFront2AngleXStep;
                else
                    if (rightLimbFront2Constraint.ControlDegAngleX > maxUp2AngleX)
                        rightLimbFront2Constraint.ControlDegAngleX -= rightFront2AngleXStep;
            }

            if (Math.Abs(rightLimbFront3Constraint.ControlDegAngleX - maxUp3AngleX) > Math.Abs(rightFront3AngleXStep))
            {
                move = true;
                if (rightLimbFront3Constraint.ControlDegAngleX < maxUp3AngleX)
                    rightLimbFront3Constraint.ControlDegAngleX += rightFront3AngleXStep;
                else
                    if (rightLimbFront3Constraint.ControlDegAngleX > maxUp3AngleX)
                        rightLimbFront3Constraint.ControlDegAngleX -= rightFront3AngleXStep;
            }



            if (Math.Abs(rightLimbMiddleFront1Constraint.ControlDegAngleX - maxUp1AngleX) > Math.Abs(rightMiddleFront1AngleXStep))
            {
                move = true;
                if (rightLimbMiddleFront1Constraint.ControlDegAngleX < maxUp1AngleX)
                    rightLimbMiddleFront1Constraint.ControlDegAngleX += rightMiddleFront1AngleXStep;
                else
                    if (rightLimbMiddleFront1Constraint.ControlDegAngleX > maxUp1AngleX)
                        rightLimbMiddleFront1Constraint.ControlDegAngleX -= rightMiddleFront1AngleXStep;
            }

            if (Math.Abs(rightLimbMiddleFront2Constraint.ControlDegAngleX - maxUp2AngleX) > Math.Abs(rightMiddleFront2AngleXStep))
            {
                move = true;
                if (rightLimbMiddleFront2Constraint.ControlDegAngleX < maxUp2AngleX)
                    rightLimbMiddleFront2Constraint.ControlDegAngleX += rightMiddleFront2AngleXStep;
                else
                    if (rightLimbMiddleFront2Constraint.ControlDegAngleX > maxUp2AngleX)
                        rightLimbMiddleFront2Constraint.ControlDegAngleX -= rightMiddleFront2AngleXStep;
            }

            if (Math.Abs(rightLimbMiddleFront3Constraint.ControlDegAngleX - maxUp3AngleX) > Math.Abs(rightMiddleFront3AngleXStep))
            {
                move = true;
                if (rightLimbMiddleFront3Constraint.ControlDegAngleX < maxUp3AngleX)
                    rightLimbMiddleFront3Constraint.ControlDegAngleX += rightMiddleFront3AngleXStep;
                else
                    if (rightLimbMiddleFront3Constraint.ControlDegAngleX > maxUp3AngleX)
                        rightLimbMiddleFront3Constraint.ControlDegAngleX -= rightMiddleFront3AngleXStep;
            }



            if (Math.Abs(rightLimbMiddleBack1Constraint.ControlDegAngleX - maxUp1AngleX) > Math.Abs(rightMiddleBack1AngleXStep))
            {
                move = true;
                if (rightLimbMiddleBack1Constraint.ControlDegAngleX < maxUp1AngleX)
                    rightLimbMiddleBack1Constraint.ControlDegAngleX += rightMiddleBack1AngleXStep;
                else
                    if (rightLimbMiddleBack1Constraint.ControlDegAngleX > maxUp1AngleX)
                        rightLimbMiddleBack1Constraint.ControlDegAngleX -= rightMiddleBack1AngleXStep;
            }

            if (Math.Abs(rightLimbMiddleBack2Constraint.ControlDegAngleX - maxUp2AngleX) > Math.Abs(rightMiddleBack2AngleXStep))
            {
                move = true;
                if (rightLimbMiddleBack2Constraint.ControlDegAngleX < maxUp2AngleX)
                    rightLimbMiddleBack2Constraint.ControlDegAngleX += rightMiddleBack2AngleXStep;
                else
                    if (rightLimbMiddleBack2Constraint.ControlDegAngleX > maxUp2AngleX)
                        rightLimbMiddleBack2Constraint.ControlDegAngleX -= rightMiddleBack2AngleXStep;
            }

            if (Math.Abs(rightLimbMiddleBack3Constraint.ControlDegAngleX - maxUp3AngleX) > Math.Abs(rightMiddleBack3AngleXStep))
            {
                move = true;
                if (rightLimbMiddleBack3Constraint.ControlDegAngleX < maxUp3AngleX)
                    rightLimbMiddleBack3Constraint.ControlDegAngleX += rightMiddleBack3AngleXStep;
                else
                    if (rightLimbMiddleBack3Constraint.ControlDegAngleX > maxUp3AngleX)
                        rightLimbMiddleBack3Constraint.ControlDegAngleX -= rightMiddleBack3AngleXStep;
            }



            if (Math.Abs(rightLimbBack1Constraint.ControlDegAngleX - maxUp1AngleX) > Math.Abs(rightBack1AngleXStep))
            {
                move = true;
                if (rightLimbBack1Constraint.ControlDegAngleX < maxUp1AngleX)
                    rightLimbBack1Constraint.ControlDegAngleX += rightBack1AngleXStep;
                else
                    if (rightLimbBack1Constraint.ControlDegAngleX > maxUp1AngleX)
                        rightLimbBack1Constraint.ControlDegAngleX -= rightBack1AngleXStep;
            }

            if (Math.Abs(rightLimbBack2Constraint.ControlDegAngleX - maxUp2AngleX) > Math.Abs(rightBack2AngleXStep))
            {
                move = true;
                if (rightLimbBack2Constraint.ControlDegAngleX < maxUp2AngleX)
                    rightLimbBack2Constraint.ControlDegAngleX += rightBack2AngleXStep;
                else
                    if (rightLimbBack2Constraint.ControlDegAngleX > maxUp2AngleX)
                        rightLimbBack2Constraint.ControlDegAngleX -= rightBack2AngleXStep;
            }

            if (Math.Abs(rightLimbBack3Constraint.ControlDegAngleX - maxUp3AngleX) > Math.Abs(rightBack3AngleXStep))
            {
                move = true;
                if (rightLimbBack3Constraint.ControlDegAngleX < maxUp3AngleX)
                    rightLimbBack3Constraint.ControlDegAngleX += rightBack3AngleXStep;
                else
                    if (rightLimbBack3Constraint.ControlDegAngleX > maxUp3AngleX)
                        rightLimbBack3Constraint.ControlDegAngleX -= rightBack3AngleXStep;
            }



            if (Math.Abs(leftLimbFront1Constraint.ControlDegAngleX + maxUp1AngleX) > Math.Abs(leftFront1AngleXStep))
            {
                move = true;
                if (leftLimbFront1Constraint.ControlDegAngleX > -maxUp1AngleX)
                    leftLimbFront1Constraint.ControlDegAngleX += leftFront1AngleXStep;
                else
                    if (leftLimbFront1Constraint.ControlDegAngleX < -maxUp1AngleX)
                        leftLimbFront1Constraint.ControlDegAngleX -= leftFront1AngleXStep;
            }

            if (Math.Abs(leftLimbFront2Constraint.ControlDegAngleX + maxUp2AngleX) > Math.Abs(leftFront2AngleXStep))
            {
                move = true;
                if (leftLimbFront2Constraint.ControlDegAngleX > -maxUp2AngleX)
                    leftLimbFront2Constraint.ControlDegAngleX += leftFront2AngleXStep;
                else
                    if (leftLimbFront2Constraint.ControlDegAngleX < -maxUp2AngleX)
                        leftLimbFront2Constraint.ControlDegAngleX -= leftFront2AngleXStep;
            }

            if (Math.Abs(leftLimbFront3Constraint.ControlDegAngleX + maxUp3AngleX) > Math.Abs(leftFront3AngleXStep))
            {
                move = true;
                if (leftLimbFront3Constraint.ControlDegAngleX > -maxUp3AngleX)
                    leftLimbFront3Constraint.ControlDegAngleX += leftFront3AngleXStep;
                else
                    if (leftLimbFront3Constraint.ControlDegAngleX < -maxUp3AngleX)
                        leftLimbFront3Constraint.ControlDegAngleX -= leftFront3AngleXStep;
            }



            if (Math.Abs(leftLimbMiddleFront1Constraint.ControlDegAngleX + maxUp1AngleX) > Math.Abs(leftMiddleFront1AngleXStep))
            {
                move = true;
                if (leftLimbMiddleFront1Constraint.ControlDegAngleX > -maxUp1AngleX)
                    leftLimbMiddleFront1Constraint.ControlDegAngleX += leftMiddleFront1AngleXStep;
                else
                    if (leftLimbMiddleFront1Constraint.ControlDegAngleX < -maxUp1AngleX)
                        leftLimbMiddleFront1Constraint.ControlDegAngleX -= leftMiddleFront1AngleXStep;
            }

            if (Math.Abs(leftLimbMiddleFront2Constraint.ControlDegAngleX + maxUp2AngleX) > Math.Abs(leftMiddleFront2AngleXStep))
            {
                move = true;
                if (leftLimbMiddleFront2Constraint.ControlDegAngleX > -maxUp2AngleX)
                    leftLimbMiddleFront2Constraint.ControlDegAngleX += leftMiddleFront2AngleXStep;
                else
                    if (leftLimbMiddleFront2Constraint.ControlDegAngleX < -maxUp2AngleX)
                        leftLimbMiddleFront2Constraint.ControlDegAngleX -= leftMiddleFront2AngleXStep;
            }

            if (Math.Abs(leftLimbMiddleFront3Constraint.ControlDegAngleX + maxUp3AngleX) > Math.Abs(leftMiddleFront3AngleXStep))
            {
                move = true;
                if (leftLimbMiddleFront3Constraint.ControlDegAngleX > -maxUp3AngleX)
                    leftLimbMiddleFront3Constraint.ControlDegAngleX += leftMiddleFront3AngleXStep;
                else
                    if (leftLimbMiddleFront3Constraint.ControlDegAngleX < -maxUp3AngleX)
                        leftLimbMiddleFront3Constraint.ControlDegAngleX -= leftMiddleFront3AngleXStep;
            }



            if (Math.Abs(leftLimbMiddleBack1Constraint.ControlDegAngleX + maxUp1AngleX) > Math.Abs(leftMiddleBack1AngleXStep))
            {
                move = true;
                if (leftLimbMiddleBack1Constraint.ControlDegAngleX > -maxUp1AngleX)
                    leftLimbMiddleBack1Constraint.ControlDegAngleX += leftMiddleBack1AngleXStep;
                else
                    if (leftLimbMiddleBack1Constraint.ControlDegAngleX < -maxUp1AngleX)
                        leftLimbMiddleBack1Constraint.ControlDegAngleX -= leftMiddleBack1AngleXStep;
            }

            if (Math.Abs(leftLimbMiddleBack2Constraint.ControlDegAngleX + maxUp2AngleX) > Math.Abs(leftMiddleBack2AngleXStep))
            {
                move = true;
                if (leftLimbMiddleBack2Constraint.ControlDegAngleX > -maxUp2AngleX)
                    leftLimbMiddleBack2Constraint.ControlDegAngleX += leftMiddleBack2AngleXStep;
                else
                    if (leftLimbMiddleBack2Constraint.ControlDegAngleX < -maxUp2AngleX)
                        leftLimbMiddleBack2Constraint.ControlDegAngleX -= leftMiddleBack2AngleXStep;
            }

            if (Math.Abs(leftLimbMiddleBack3Constraint.ControlDegAngleX + maxUp3AngleX) > Math.Abs(leftMiddleBack3AngleXStep))
            {
                move = true;
                if (leftLimbMiddleBack3Constraint.ControlDegAngleX > -maxUp3AngleX)
                    leftLimbMiddleBack3Constraint.ControlDegAngleX += leftMiddleBack3AngleXStep;
                else
                    if (leftLimbMiddleBack3Constraint.ControlDegAngleX < -maxUp3AngleX)
                        leftLimbMiddleBack3Constraint.ControlDegAngleX -= leftMiddleBack3AngleXStep;
            }


            if (Math.Abs(leftLimbBack1Constraint.ControlDegAngleX + maxUp1AngleX) > Math.Abs(leftBack1AngleXStep))
            {
                move = true;
                if (leftLimbBack1Constraint.ControlDegAngleX > -maxUp1AngleX)
                    leftLimbBack1Constraint.ControlDegAngleX += leftBack1AngleXStep;
                else
                    if (leftLimbBack1Constraint.ControlDegAngleX < -maxUp1AngleX)
                        leftLimbBack1Constraint.ControlDegAngleX -= leftBack1AngleXStep;
            }

            if (Math.Abs(leftLimbBack2Constraint.ControlDegAngleX + maxUp2AngleX) > Math.Abs(leftBack2AngleXStep))
            {
                move = true;
                if (leftLimbBack2Constraint.ControlDegAngleX > -maxUp2AngleX)
                    leftLimbBack2Constraint.ControlDegAngleX += leftBack2AngleXStep;
                else
                    if (leftLimbBack2Constraint.ControlDegAngleX < -maxUp2AngleX)
                        leftLimbBack2Constraint.ControlDegAngleX -= leftBack2AngleXStep;
            }

            if (Math.Abs(leftLimbBack3Constraint.ControlDegAngleX + maxUp3AngleX) > Math.Abs(leftBack3AngleXStep))
            {
                move = true;
                if (leftLimbBack3Constraint.ControlDegAngleX > -maxUp3AngleX)
                    leftLimbBack3Constraint.ControlDegAngleX += leftBack3AngleXStep;
                else
                    if (leftLimbBack3Constraint.ControlDegAngleX < -maxUp3AngleX)
                        leftLimbBack3Constraint.ControlDegAngleX -= leftBack3AngleXStep;
            }

            moveUp = move;
        }

        void GoAhead(SimulateMethodArgs args)
        {
            PhysicsScene scene = demo.Engine.Factory.PhysicsSceneManager.Get(args.OwnerSceneIndex);
            PhysicsObject objectBase = scene.Factory.PhysicsObjectManager.Get(args.OwnerIndex);

            float time = args.Time;

            if (rightFront1AngleXStep > 0.0f)
            {
                if (rightLimbFront1Constraint.ControlDegAngleX <= rightLimbFront1Constraint.MinLimitDegAngleX)
                {
                    if (rightLimbFront1Constraint.ControlDegAngleY >= maxAhead1AngleY)
                    {
                        rightFront1AngleXStep = -rightFront1AngleXStep;
                        rightFront1AngleYStep = -rightFront1AngleYStep;
                    }
                    else
                        rightLimbFront1Constraint.ControlDegAngleY += rightFront1AngleYStep;
                }
                else
                    rightLimbFront1Constraint.ControlDegAngleX -= rightFront1AngleXStep;
            }
            else
                if (rightFront1AngleXStep < 0.0f)
                {
                    if (rightLimbFront1Constraint.ControlDegAngleX >= maxUp1AngleX)
                    {
                        if (rightLimbFront1Constraint.ControlDegAngleY <= maxAhead2AngleY)
                        {
                            rightFront1AngleXStep = -rightFront1AngleXStep;
                            rightFront1AngleYStep = -rightFront1AngleYStep;
                        }
                        else
                            rightLimbFront1Constraint.ControlDegAngleY += rightFront1AngleYStep;
                    }
                    else
                        rightLimbFront1Constraint.ControlDegAngleX -= rightFront1AngleXStep;
                }


            if ((rightMiddleFront1AngleXStep > 0.0f) && (leftFront1AngleXStep < 0.0f))
            {
                if (rightLimbMiddleFront1Constraint.ControlDegAngleX <= rightLimbMiddleFront1Constraint.MinLimitDegAngleX)
                {
                    if (rightLimbMiddleFront1Constraint.ControlDegAngleY >= maxAhead2AngleY)
                    {
                        rightMiddleFront1AngleXStep = -rightMiddleFront1AngleXStep;
                        rightMiddleFront1AngleYStep = -rightMiddleFront1AngleYStep;
                    }
                    else
                        rightLimbMiddleFront1Constraint.ControlDegAngleY += rightMiddleFront1AngleYStep;
                }
                else
                    rightLimbMiddleFront1Constraint.ControlDegAngleX -= rightMiddleFront1AngleXStep;
            }
            else
                if ((rightMiddleFront1AngleXStep < 0.0f) && (leftFront1AngleXStep > 0.0f))
                {
                    if (rightLimbMiddleFront1Constraint.ControlDegAngleX >= maxUp1AngleX)
                    {
                        if (rightLimbMiddleFront1Constraint.ControlDegAngleY <= maxAhead3AngleY)
                        {
                            rightMiddleFront1AngleXStep = -rightMiddleFront1AngleXStep;
                            rightMiddleFront1AngleYStep = -rightMiddleFront1AngleYStep;
                        }
                        else
                            rightLimbMiddleFront1Constraint.ControlDegAngleY += rightMiddleFront1AngleYStep;
                    }
                    else
                        rightLimbMiddleFront1Constraint.ControlDegAngleX -= rightMiddleFront1AngleXStep;
                }


            if ((rightMiddleBack1AngleXStep > 0.0f) && (leftMiddleFront1AngleXStep < 0.0f))
            {
                if (rightLimbMiddleBack1Constraint.ControlDegAngleX <= rightLimbMiddleBack1Constraint.MinLimitDegAngleX)
                {
                    if (rightLimbMiddleBack1Constraint.ControlDegAngleY >= maxAhead3AngleY)
                    {
                        rightMiddleBack1AngleXStep = -rightMiddleBack1AngleXStep;
                        rightMiddleBack1AngleYStep = -rightMiddleBack1AngleYStep;
                    }
                    else
                        rightLimbMiddleBack1Constraint.ControlDegAngleY += rightMiddleBack1AngleYStep;
                }
                else
                    rightLimbMiddleBack1Constraint.ControlDegAngleX -= rightMiddleBack1AngleXStep;
            }
            else
                if ((rightMiddleBack1AngleXStep < 0.0f) && (leftMiddleFront1AngleXStep > 0.0f))
                {
                    if (rightLimbMiddleBack1Constraint.ControlDegAngleX >= maxUp1AngleX)
                    {
                        if (rightLimbMiddleBack1Constraint.ControlDegAngleY <= maxAhead4AngleY)
                        {
                            rightMiddleBack1AngleXStep = -rightMiddleBack1AngleXStep;
                            rightMiddleBack1AngleYStep = -rightMiddleBack1AngleYStep;
                        }
                        else
                            rightLimbMiddleBack1Constraint.ControlDegAngleY += rightMiddleBack1AngleYStep;
                    }
                    else
                        rightLimbMiddleBack1Constraint.ControlDegAngleX -= rightMiddleBack1AngleXStep;
                }


            if ((rightBack1AngleXStep > 0.0f) && (leftMiddleBack1AngleXStep < 0.0f))
            {
                if (rightLimbBack1Constraint.ControlDegAngleX <= rightLimbBack1Constraint.MinLimitDegAngleX)
                {
                    if (rightLimbBack1Constraint.ControlDegAngleY >= maxAhead4AngleY)
                    {
                        rightBack1AngleXStep = -rightBack1AngleXStep;
                        rightBack1AngleYStep = -rightBack1AngleYStep;
                    }
                    else
                        rightLimbBack1Constraint.ControlDegAngleY += rightBack1AngleYStep;
                }
                else
                    rightLimbBack1Constraint.ControlDegAngleX -= rightBack1AngleXStep;
            }
            else
                if ((rightBack1AngleXStep < 0.0f) && (leftMiddleBack1AngleXStep > 0.0f))
                {
                    if (rightLimbBack1Constraint.ControlDegAngleX >= maxUp1AngleX)
                    {
                        if (rightLimbBack1Constraint.ControlDegAngleY <= maxAhead5AngleY)
                        {
                            rightBack1AngleXStep = -rightBack1AngleXStep;
                            rightBack1AngleYStep = -rightBack1AngleYStep;
                        }
                        else
                            rightLimbBack1Constraint.ControlDegAngleY += rightBack1AngleYStep;
                    }
                    else
                        rightLimbBack1Constraint.ControlDegAngleX -= rightBack1AngleXStep;
                }



            if ((leftFront1AngleXStep < 0.0f) && (rightFront1AngleXStep < 0.0f))
            {
                if (leftLimbFront1Constraint.ControlDegAngleX >= leftLimbFront1Constraint.MaxLimitDegAngleX)
                {
                    if (leftLimbFront1Constraint.ControlDegAngleY <= -maxAhead1AngleY)
                    {
                        leftFront1AngleXStep = -leftFront1AngleXStep;
                        leftFront1AngleYStep = -leftFront1AngleYStep;
                    }
                    else
                        leftLimbFront1Constraint.ControlDegAngleY += leftFront1AngleYStep;
                }
                else
                    leftLimbFront1Constraint.ControlDegAngleX -= leftFront1AngleXStep;
            }
            else
                if ((leftFront1AngleXStep > 0.0f) && (rightFront1AngleXStep > 0.0f))
                {
                    if (leftLimbFront1Constraint.ControlDegAngleX <= -maxUp1AngleX)
                    {
                        if (leftLimbFront1Constraint.ControlDegAngleY >= -maxAhead2AngleY)
                        {
                            leftFront1AngleXStep = -leftFront1AngleXStep;
                            leftFront1AngleYStep = -leftFront1AngleYStep;
                        }
                        else
                            leftLimbFront1Constraint.ControlDegAngleY += leftFront1AngleYStep;
                    }
                    else
                        leftLimbFront1Constraint.ControlDegAngleX -= leftFront1AngleXStep;
                }


            if ((leftMiddleFront1AngleXStep < 0.0f) && (rightFront1AngleXStep > 0.0f))
            {
                if (leftLimbMiddleFront1Constraint.ControlDegAngleX >= leftLimbMiddleFront1Constraint.MaxLimitDegAngleX)
                {
                    if (leftLimbMiddleFront1Constraint.ControlDegAngleY <= -maxAhead2AngleY)
                    {
                        leftMiddleFront1AngleXStep = -leftMiddleFront1AngleXStep;
                        leftMiddleFront1AngleYStep = -leftMiddleFront1AngleYStep;
                    }
                    else
                        leftLimbMiddleFront1Constraint.ControlDegAngleY += leftMiddleFront1AngleYStep;
                }
                else
                    leftLimbMiddleFront1Constraint.ControlDegAngleX -= leftMiddleFront1AngleXStep;
            }
            else
                if ((leftMiddleFront1AngleXStep > 0.0f) && (rightFront1AngleXStep < 0.0f))
                {
                    if (leftLimbMiddleFront1Constraint.ControlDegAngleX <= -maxUp1AngleX)
                    {
                        if (leftLimbMiddleFront1Constraint.ControlDegAngleY >= maxAhead3AngleY)
                        {
                            leftMiddleFront1AngleXStep = -leftMiddleFront1AngleXStep;
                            leftMiddleFront1AngleYStep = -leftMiddleFront1AngleYStep;
                        }
                        else
                            leftLimbMiddleFront1Constraint.ControlDegAngleY += leftMiddleFront1AngleYStep;
                    }
                    else
                        leftLimbMiddleFront1Constraint.ControlDegAngleX -= leftMiddleFront1AngleXStep;
                }


            if ((leftMiddleBack1AngleXStep < 0.0f) && (rightMiddleFront1AngleXStep > 0.0f))
            {
                if (leftLimbMiddleBack1Constraint.ControlDegAngleX >= leftLimbMiddleBack1Constraint.MaxLimitDegAngleX)
                {
                    if (leftLimbMiddleBack1Constraint.ControlDegAngleY <= maxAhead3AngleY)
                    {
                        leftMiddleBack1AngleXStep = -leftMiddleBack1AngleXStep;
                        leftMiddleBack1AngleYStep = -leftMiddleBack1AngleYStep;
                    }
                    else
                        leftLimbMiddleBack1Constraint.ControlDegAngleY += leftMiddleBack1AngleYStep;
                }
                else
                    leftLimbMiddleBack1Constraint.ControlDegAngleX -= leftMiddleBack1AngleXStep;
            }
            else
                if ((leftMiddleBack1AngleXStep > 0.0f) && (rightMiddleFront1AngleXStep < 0.0f))
                {
                    if (leftLimbMiddleBack1Constraint.ControlDegAngleX <= -maxUp1AngleX)
                    {
                        if (leftLimbMiddleBack1Constraint.ControlDegAngleY >= -maxAhead4AngleY)
                        {
                            leftMiddleBack1AngleXStep = -leftMiddleBack1AngleXStep;
                            leftMiddleBack1AngleYStep = -leftMiddleBack1AngleYStep;
                        }
                        else
                            leftLimbMiddleBack1Constraint.ControlDegAngleY += leftMiddleBack1AngleYStep;
                    }
                    else
                        leftLimbMiddleBack1Constraint.ControlDegAngleX -= leftMiddleBack1AngleXStep;
                }


            if ((leftBack1AngleXStep < 0.0f) && (rightMiddleBack1AngleXStep > 0.0f))
            {
                if (leftLimbBack1Constraint.ControlDegAngleX >= leftLimbBack1Constraint.MaxLimitDegAngleX)
                {
                    if (leftLimbBack1Constraint.ControlDegAngleY <= -maxAhead4AngleY)
                    {
                        leftBack1AngleXStep = -leftBack1AngleXStep;
                        leftBack1AngleYStep = -leftBack1AngleYStep;
                    }
                    else
                        leftLimbBack1Constraint.ControlDegAngleY += leftBack1AngleYStep;
                }
                else
                    leftLimbBack1Constraint.ControlDegAngleX -= leftBack1AngleXStep;
            }
            else
                if ((leftBack1AngleXStep > 0.0f) && (rightMiddleBack1AngleXStep < 0.0f))
                {
                    if (leftLimbBack1Constraint.ControlDegAngleX <= -maxUp1AngleX)
                    {
                        if (leftLimbBack1Constraint.ControlDegAngleY >= -maxAhead5AngleY)
                        {
                            leftBack1AngleXStep = -leftBack1AngleXStep;
                            leftBack1AngleYStep = -leftBack1AngleYStep;
                        }
                        else
                            leftLimbBack1Constraint.ControlDegAngleY += leftBack1AngleYStep;
                    }
                    else
                        leftLimbBack1Constraint.ControlDegAngleX -= leftBack1AngleXStep;
                }
        }
    }
}
