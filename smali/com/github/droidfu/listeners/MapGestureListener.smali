.class public Lcom/github/droidfu/listeners/MapGestureListener;
.super Landroid/view/GestureDetector$SimpleOnGestureListener;
.source "MapGestureListener.java"


# instance fields
.field protected mapActivity:Lcom/github/droidfu/activities/BetterMapActivity;


# direct methods
.method public constructor <init>(Lcom/github/droidfu/activities/BetterMapActivity;)V
    .locals 0

    .line 13
    invoke-direct {p0}, Landroid/view/GestureDetector$SimpleOnGestureListener;-><init>()V

    .line 14
    iput-object p1, p0, Lcom/github/droidfu/listeners/MapGestureListener;->mapActivity:Lcom/github/droidfu/activities/BetterMapActivity;

    return-void
.end method


# virtual methods
.method public onDoubleTap(Landroid/view/MotionEvent;)Z
    .locals 2

    .line 19
    iget-object v0, p0, Lcom/github/droidfu/listeners/MapGestureListener;->mapActivity:Lcom/github/droidfu/activities/BetterMapActivity;

    invoke-virtual {v0}, Lcom/github/droidfu/activities/BetterMapActivity;->getMapView()Lcom/google/android/maps/MapView;

    move-result-object v0

    invoke-virtual {v0}, Lcom/google/android/maps/MapView;->getController()Lcom/google/android/maps/MapController;

    move-result-object v0

    invoke-virtual {p1}, Landroid/view/MotionEvent;->getX()F

    move-result v1

    float-to-int v1, v1

    invoke-virtual {p1}, Landroid/view/MotionEvent;->getY()F

    move-result p1

    float-to-int p1, p1

    invoke-virtual {v0, v1, p1}, Lcom/google/android/maps/MapController;->zoomInFixing(II)Z

    const/4 p1, 0x1

    return p1
.end method
