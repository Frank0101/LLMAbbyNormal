# Deriving the Gradients of a Single Neuron

In this example, we use a single neuron with an identity activation function.
Its predicted output is therefore:

$$
Y_p = XW + B
$$

where:

- $X$ is the input;
- $W$ is the weight;
- $B$ is the bias;
- $Y_p$ is the predicted output.

Different activation functions are used when, for example, a neural network
needs to learn non-linear relationships. In this experiment we are learning the
linear relationship:

$$
Y = 2X
$$

We want to understand how linear regression can be trained with gradient
descent. A gradient tells us how much the loss changes when we change one of the
model's parameters by an infinitesimally small quantity.

We will call this small change $H$ and consider what happens as $H$ approaches
zero.

## Prediction and error

We define the error $E$ as the predicted value minus the expected value:

$$
E = Y_p - Y_e
$$

where $Y_e$ is the expected output. Because $Y_p = XW + B$, we can also write:

$$
E = XW + B - Y_e
$$

For example, with:

$$
X = 2, \qquad W = 1, \qquad B = 0, \qquad Y_e = 4
$$

the neuron produces:

$$
\begin{aligned}
Y_p &= 2 \cdot 1 + 0 = 2 \\
E &= 2 - 4 = -2
\end{aligned}
$$

## The loss function

For one training example, we use the half squared error:

$$
L = \frac{E^2}{2}
$$

When this loss is averaged over the complete dataset, it is half the mean
squared error. Multiplying the mean squared error by $\frac{1}{2}$ does not
change where its minimum occurs; it only simplifies the gradient calculation,
as we will see below.

To calculate the gradient with respect to either $W$ or $B$, we split the
calculation into two parts:

1. How $L$ changes when $E$ changes.
2. How $E$ changes when $W$ or $B$ changes.

We then multiply those changes using the chain rule.

## How the loss changes with the error

The gradient of $L$ with respect to $E$ is written as
$\frac{\partial L}{\partial E}$. The symbol $\partial$ means that we are
measuring the change with respect to one particular quantity while treating the
others as fixed.

The gradient is the change in the loss divided by a small change $H$ in the
error, in the limit where $H$ approaches zero:

$$
\frac{\partial L}{\partial E}
= \lim_{H \to 0}\frac{L(E+H)-L(E)}{H}
$$

We know that:

$$
L(E) = \frac{E^2}{2}
$$

and:

$$
L(E+H)
= \frac{(E+H)^2}{2}
= \frac{E^2+2EH+H^2}{2}
$$

Substitute these expressions into the gradient calculation:

$$
\begin{aligned}
\frac{\partial L}{\partial E}
&= \lim_{H \to 0}
   \frac{\frac{E^2+2EH+H^2}{2}-\frac{E^2}{2}}{H} \\
&= \lim_{H \to 0}\frac{EH+\frac{H^2}{2}}{H} \\
&= \lim_{H \to 0}\left(E+\frac{H}{2}\right) \\
&= E
\end{aligned}
$$

This is why we divide the squared error by $2$. If we had used $L=E^2$,
this gradient would have been $2E$ instead.

## How the error changes with the weight

We now calculate how $E$ changes when we increase $W$ by $H$. This gradient is
written as $\frac{\partial E}{\partial W}$:

$$
\frac{\partial E}{\partial W}
= \lim_{H \to 0}\frac{E(W+H)-E(W)}{H}
$$

We know that:

$$
\begin{aligned}
E(W) &= XW+B-Y_e \\
E(W+H) &= X(W+H)+B-Y_e
\end{aligned}
$$

Substitute these expressions into the gradient calculation:

$$
\begin{aligned}
\frac{\partial E}{\partial W}
&= \lim_{H \to 0}
   \frac{X(W+H)+B-Y_e-(XW+B-Y_e)}{H} \\
&= \lim_{H \to 0}
   \frac{XW+XH+B-Y_e-XW-B+Y_e}{H} \\
&= \lim_{H \to 0}\frac{XH}{H} \\
&= X
\end{aligned}
$$

We can now use the chain rule to calculate how the loss changes with the
weight:

$$
\begin{aligned}
\frac{\partial L}{\partial W}
&= \frac{\partial L}{\partial E}
   \frac{\partial E}{\partial W} \\
&= E \cdot X
\end{aligned}
$$

Therefore:

$$
\boxed{\text{weightGradient}=EX}
$$

## How the error changes with the bias

We follow the same process for $B$. Increase the bias by $H$ and calculate
$\frac{\partial E}{\partial B}$:

$$
\frac{\partial E}{\partial B}
= \lim_{H \to 0}\frac{E(B+H)-E(B)}{H}
$$

We know that:

$$
\begin{aligned}
E(B) &= XW+B-Y_e \\
E(B+H) &= XW+(B+H)-Y_e
\end{aligned}
$$

Substitute these expressions into the gradient calculation:

$$
\begin{aligned}
\frac{\partial E}{\partial B}
&= \lim_{H \to 0}
   \frac{XW+B+H-Y_e-(XW+B-Y_e)}{H} \\
&= \lim_{H \to 0}
   \frac{XW+B+H-Y_e-XW-B+Y_e}{H} \\
&= \lim_{H \to 0}\frac{H}{H} \\
&= 1
\end{aligned}
$$

Apply the chain rule again:

$$
\begin{aligned}
\frac{\partial L}{\partial B}
&= \frac{\partial L}{\partial E}
   \frac{\partial E}{\partial B} \\
&= E \cdot 1 \\
&= E
\end{aligned}
$$

Therefore:

$$
\boxed{\text{biasGradient}=E}
$$

## Final gradients

The two gradients are:

$$
\boxed{
\frac{\partial L}{\partial W}=EX,
\qquad
\frac{\partial L}{\partial B}=E
}
$$

With the example values $E=-2$ and $X=2$:

$$
\begin{aligned}
\text{weightGradient} &= -2 \cdot 2 = -4 \\
\text{biasGradient} &= -2
\end{aligned}
$$

These are the calculations implemented in the training experiment:

```csharp
totalWeightGradient += error * example.X;
totalBiasGradient += error;
```

The training process averages these gradients over the dataset and uses them to
update the weight and bias in the direction that reduces the loss.
