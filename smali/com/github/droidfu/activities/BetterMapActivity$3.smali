.class Lcom/github/droidfu/activities/BetterMapActivity$3;
.super Ljava/lang/Object;
.source "BetterMapActivity.java"

# interfaces
.implements Landroid/view/View$OnTouchListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/github/droidfu/activities/BetterMapActivity;->setMapGestureListener(Lcom/github/droidfu/listeners/MapGestureListener;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/github/droidfu/activities/BetterMapActivity;


# direct methods
.method constructor <init>(Lcom/github/droidfu/activities/BetterMapActivity;)V
    .locals 0

    .line 1
    iput-object p1, p0, Lcom/github/droidfu/activities/BetterMapActivity$3;->this$0:Lcom/github/droidfu/activities/BetterMapActivity;

    .line 208
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onTouch(Landroid/view/View;Landroid/view/MotionEvent;)Z
    .locals 0

    .line 210
    iget-object p1, p0, Lcom/github/droidfu/activities/BetterMapActivity$3;->this$0:Lcom/github/droidfu/activities/BetterMapActivity;

    invoke-static {p1}, Lcom/github/droidfu/activities/BetterMapActivity;->access$0(Lcom/github/droidfu/activities/BetterMapActivity;)Landroid/view/GestureDetector;

    move-result-object p1

    invoke-virtual {p1, p2}, Landroid/view/GestureDetector;->onTouchEvent(Landroid/view/MotionEvent;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/4 p1, 0x1

    return p1

    :cond_0
    const/4 p1, 0x0

    return p1
.end method
