.class Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;
.super Ljava/lang/Object;
.source "NewsFragment.java"

# interfaces
.implements Landroid/view/View$OnTouchListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field private final gestureDetector:Landroid/view/GestureDetector;

.field final synthetic this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)V
    .locals 2

    .line 144
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 146
    new-instance p1, Landroid/view/GestureDetector;

    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    new-instance v1, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4$1;

    invoke-direct {v1, p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4$1;-><init>(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;)V

    invoke-direct {p1, v0, v1}, Landroid/view/GestureDetector;-><init>(Landroid/content/Context;Landroid/view/GestureDetector$OnGestureListener;)V

    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;->gestureDetector:Landroid/view/GestureDetector;

    return-void
.end method


# virtual methods
.method public onTouch(Landroid/view/View;Landroid/view/MotionEvent;)Z
    .locals 0

    .line 196
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;->gestureDetector:Landroid/view/GestureDetector;

    invoke-virtual {p1, p2}, Landroid/view/GestureDetector;->onTouchEvent(Landroid/view/MotionEvent;)Z

    move-result p1

    return p1
.end method
