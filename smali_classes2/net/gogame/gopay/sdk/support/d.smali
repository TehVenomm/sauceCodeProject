.class final Lnet/gogame/gopay/sdk/support/d;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/view/View$OnTouchListener;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/support/c;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/support/c;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/support/d;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final onTouch(Landroid/view/View;Landroid/view/MotionEvent;)Z
    .locals 0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/d;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/support/c;->a(Lnet/gogame/gopay/sdk/support/c;)Landroid/view/GestureDetector;

    move-result-object p1

    invoke-virtual {p1, p2}, Landroid/view/GestureDetector;->onTouchEvent(Landroid/view/MotionEvent;)Z

    move-result p1

    return p1
.end method
