.class Lcom/github/droidfu/activities/BetterMapActivity$1;
.super Ljava/lang/Object;
.source "BetterMapActivity.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/github/droidfu/activities/BetterMapActivity;->setMapViewWithZoom(II)V
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
    iput-object p1, p0, Lcom/github/droidfu/activities/BetterMapActivity$1;->this$0:Lcom/github/droidfu/activities/BetterMapActivity;

    .line 177
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 2

    .line 179
    iget-object p1, p0, Lcom/github/droidfu/activities/BetterMapActivity$1;->this$0:Lcom/github/droidfu/activities/BetterMapActivity;

    invoke-virtual {p1}, Lcom/github/droidfu/activities/BetterMapActivity;->getMapView()Lcom/google/android/maps/MapView;

    move-result-object p1

    invoke-virtual {p1}, Lcom/google/android/maps/MapView;->getController()Lcom/google/android/maps/MapController;

    move-result-object p1

    iget-object v0, p0, Lcom/github/droidfu/activities/BetterMapActivity$1;->this$0:Lcom/github/droidfu/activities/BetterMapActivity;

    invoke-virtual {v0}, Lcom/github/droidfu/activities/BetterMapActivity;->getMapView()Lcom/google/android/maps/MapView;

    move-result-object v0

    invoke-virtual {v0}, Lcom/google/android/maps/MapView;->getWidth()I

    move-result v0

    div-int/lit8 v0, v0, 0x2

    .line 180
    iget-object v1, p0, Lcom/github/droidfu/activities/BetterMapActivity$1;->this$0:Lcom/github/droidfu/activities/BetterMapActivity;

    invoke-virtual {v1}, Lcom/github/droidfu/activities/BetterMapActivity;->getMapView()Lcom/google/android/maps/MapView;

    move-result-object v1

    invoke-virtual {v1}, Lcom/google/android/maps/MapView;->getHeight()I

    move-result v1

    div-int/lit8 v1, v1, 0x2

    .line 179
    invoke-virtual {p1, v0, v1}, Lcom/google/android/maps/MapController;->zoomInFixing(II)Z

    return-void
.end method
