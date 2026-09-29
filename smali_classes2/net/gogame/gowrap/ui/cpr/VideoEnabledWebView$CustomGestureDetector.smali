.class Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$CustomGestureDetector;
.super Landroid/view/GestureDetector$SimpleOnGestureListener;
.source "VideoEnabledWebView.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x2
    name = "CustomGestureDetector"
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;


# direct methods
.method private constructor <init>(Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;)V
    .locals 0

    .line 134
    iput-object p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$CustomGestureDetector;->this$0:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    invoke-direct {p0}, Landroid/view/GestureDetector$SimpleOnGestureListener;-><init>()V

    return-void
.end method

.method synthetic constructor <init>(Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$1;)V
    .locals 0

    .line 134
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$CustomGestureDetector;-><init>(Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;)V

    return-void
.end method


# virtual methods
.method public onFling(Landroid/view/MotionEvent;Landroid/view/MotionEvent;FF)Z
    .locals 0

    const/4 p1, 0x1

    return p1
.end method
