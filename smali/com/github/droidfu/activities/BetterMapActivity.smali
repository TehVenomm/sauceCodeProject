.class public Lcom/github/droidfu/activities/BetterMapActivity;
.super Lcom/google/android/maps/MapActivity;
.source "BetterMapActivity.java"

# interfaces
.implements Lcom/github/droidfu/activities/BetterActivity;


# instance fields
.field private currentIntent:Landroid/content/Intent;

.field private mapView:Lcom/google/android/maps/MapView;

.field private myLocationOverlay:Lcom/google/android/maps/MyLocationOverlay;

.field private progressDialogMsgId:I

.field private progressDialogTitleId:I

.field private tapDetector:Landroid/view/GestureDetector;

.field private tapListener:Landroid/view/View$OnTouchListener;

.field private wasCreated:Z

.field private wasInterrupted:Z


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 25
    invoke-direct {p0}, Lcom/google/android/maps/MapActivity;-><init>()V

    return-void
.end method

.method static synthetic access$0(Lcom/github/droidfu/activities/BetterMapActivity;)Landroid/view/GestureDetector;
    .locals 0

    .line 37
    iget-object p0, p0, Lcom/github/droidfu/activities/BetterMapActivity;->tapDetector:Landroid/view/GestureDetector;

    return-object p0
.end method


# virtual methods
.method public getCurrentIntent()Landroid/content/Intent;
    .locals 1

    .line 93
    iget-object v0, p0, Lcom/github/droidfu/activities/BetterMapActivity;->currentIntent:Landroid/content/Intent;

    return-object v0
.end method

.method public getMapView()Lcom/google/android/maps/MapView;
    .locals 1

    .line 191
    iget-object v0, p0, Lcom/github/droidfu/activities/BetterMapActivity;->mapView:Lcom/google/android/maps/MapView;

    return-object v0
.end method

.method public getMyLocationOverlay()Lcom/google/android/maps/MyLocationOverlay;
    .locals 1

    .line 201
    iget-object v0, p0, Lcom/github/droidfu/activities/BetterMapActivity;->myLocationOverlay:Lcom/google/android/maps/MyLocationOverlay;

    return-object v0
.end method

.method public getWindowFeatures()I
    .locals 1

    .line 97
    invoke-static {p0}, Lcom/github/droidfu/activities/BetterActivityHelper;->getWindowFeatures(Landroid/app/Activity;)I

    move-result v0

    return v0
.end method

.method public isApplicationBroughtToBackground()Z
    .locals 1

    .line 101
    invoke-static {p0}, Lcom/github/droidfu/activities/BetterActivityHelper;->isApplicationBroughtToBackground(Landroid/content/Context;)Z

    move-result v0

    return v0
.end method

.method public isLandscapeMode()Z
    .locals 2

    .line 105
    invoke-virtual {p0}, Lcom/github/droidfu/activities/BetterMapActivity;->getWindowManager()Landroid/view/WindowManager;

    move-result-object v0

    invoke-interface {v0}, Landroid/view/WindowManager;->getDefaultDisplay()Landroid/view/Display;

    move-result-object v0

    invoke-virtual {v0}, Landroid/view/Display;->getOrientation()I

    move-result v0

    const/4 v1, 0x1

    if-ne v0, v1, :cond_0

    return v1

    :cond_0
    const/4 v0, 0x0

    return v0
.end method

.method public isLaunching()Z
    .locals 1

    .line 109
    iget-boolean v0, p0, Lcom/github/droidfu/activities/BetterMapActivity;->wasInterrupted:Z

    if-nez v0, :cond_0

    iget-boolean v0, p0, Lcom/github/droidfu/activities/BetterMapActivity;->wasCreated:Z

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    return v0

    :cond_0
    const/4 v0, 0x0

    return v0
.end method

.method public isPortraitMode()Z
    .locals 1

    .line 113
    invoke-virtual {p0}, Lcom/github/droidfu/activities/BetterMapActivity;->isLandscapeMode()Z

    move-result v0

    xor-int/lit8 v0, v0, 0x1

    return v0
.end method

.method public isRestoring()Z
    .locals 1

    .line 117
    iget-boolean v0, p0, Lcom/github/droidfu/activities/BetterMapActivity;->wasInterrupted:Z

    return v0
.end method

.method public isResuming()Z
    .locals 1

    .line 121
    iget-boolean v0, p0, Lcom/github/droidfu/activities/BetterMapActivity;->wasCreated:Z

    xor-int/lit8 v0, v0, 0x1

    return v0
.end method

.method protected isRouteDisplayed()Z
    .locals 1

    const/4 v0, 0x0

    return v0
.end method

.method public newAlertDialog(II)Landroid/app/AlertDialog;
    .locals 1

    .line 136
    invoke-virtual {p0, p1}, Lcom/github/droidfu/activities/BetterMapActivity;->getString(I)Ljava/lang/String;

    move-result-object p1

    .line 137
    invoke-virtual {p0, p2}, Lcom/github/droidfu/activities/BetterMapActivity;->getString(I)Ljava/lang/String;

    move-result-object p2

    const v0, 0x1080027

    .line 136
    invoke-static {p0, p1, p2, v0}, Lcom/github/droidfu/activities/BetterActivityHelper;->newMessageDialog(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;I)Landroid/app/AlertDialog;

    move-result-object p1

    return-object p1
.end method

.method public newErrorHandlerDialog(ILjava/lang/Exception;)Landroid/app/AlertDialog;
    .locals 0

    .line 141
    invoke-virtual {p0, p1}, Lcom/github/droidfu/activities/BetterMapActivity;->getString(I)Ljava/lang/String;

    move-result-object p1

    invoke-static {p0, p1, p2}, Lcom/github/droidfu/activities/BetterActivityHelper;->newErrorHandlerDialog(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/Exception;)Landroid/app/AlertDialog;

    move-result-object p1

    return-object p1
.end method

.method public newErrorHandlerDialog(Ljava/lang/Exception;)Landroid/app/AlertDialog;
    .locals 4

    .line 145
    invoke-virtual {p0}, Lcom/github/droidfu/activities/BetterMapActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v1, "droidfu_error_dialog_title"

    const-string v2, "string"

    .line 146
    invoke-virtual {p0}, Lcom/github/droidfu/activities/BetterMapActivity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    .line 145
    invoke-virtual {v0, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    invoke-virtual {p0, v0, p1}, Lcom/github/droidfu/activities/BetterMapActivity;->newErrorHandlerDialog(ILjava/lang/Exception;)Landroid/app/AlertDialog;

    move-result-object p1

    return-object p1
.end method

.method public newInfoDialog(II)Landroid/app/AlertDialog;
    .locals 1

    .line 131
    invoke-virtual {p0, p1}, Lcom/github/droidfu/activities/BetterMapActivity;->getString(I)Ljava/lang/String;

    move-result-object p1

    .line 132
    invoke-virtual {p0, p2}, Lcom/github/droidfu/activities/BetterMapActivity;->getString(I)Ljava/lang/String;

    move-result-object p2

    const v0, 0x108009b

    .line 131
    invoke-static {p0, p1, p2, v0}, Lcom/github/droidfu/activities/BetterActivityHelper;->newMessageDialog(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;I)Landroid/app/AlertDialog;

    move-result-object p1

    return-object p1
.end method

.method public newListDialog(Ljava/lang/String;Ljava/util/List;Lcom/github/droidfu/dialogs/DialogClickListener;Z)Landroid/app/Dialog;
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<T:",
            "Ljava/lang/Object;",
            ">(",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "TT;>;",
            "Lcom/github/droidfu/dialogs/DialogClickListener<",
            "TT;>;Z)",
            "Landroid/app/Dialog;"
        }
    .end annotation

    .line 152
    invoke-static {p0, p1, p2, p3, p4}, Lcom/github/droidfu/activities/BetterActivityHelper;->newListDialog(Landroid/app/Activity;Ljava/lang/String;Ljava/util/List;Lcom/github/droidfu/dialogs/DialogClickListener;Z)Landroid/app/Dialog;

    move-result-object p1

    return-object p1
.end method

.method public newYesNoDialog(IILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog;
    .locals 1

    .line 126
    invoke-virtual {p0, p1}, Lcom/github/droidfu/activities/BetterMapActivity;->getString(I)Ljava/lang/String;

    move-result-object p1

    .line 127
    invoke-virtual {p0, p2}, Lcom/github/droidfu/activities/BetterMapActivity;->getString(I)Ljava/lang/String;

    move-result-object p2

    const v0, 0x108009b

    .line 126
    invoke-static {p0, p1, p2, v0, p3}, Lcom/github/droidfu/activities/BetterActivityHelper;->newYesNoDialog(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog;

    move-result-object p1

    return-object p1
.end method

.method protected onCreate(Landroid/os/Bundle;)V
    .locals 1

    .line 45
    invoke-super {p0, p1}, Lcom/google/android/maps/MapActivity;->onCreate(Landroid/os/Bundle;)V

    const/4 p1, 0x1

    .line 47
    iput-boolean p1, p0, Lcom/github/droidfu/activities/BetterMapActivity;->wasCreated:Z

    .line 48
    invoke-virtual {p0}, Lcom/github/droidfu/activities/BetterMapActivity;->getIntent()Landroid/content/Intent;

    move-result-object p1

    iput-object p1, p0, Lcom/github/droidfu/activities/BetterMapActivity;->currentIntent:Landroid/content/Intent;

    .line 50
    invoke-virtual {p0}, Lcom/github/droidfu/activities/BetterMapActivity;->getApplication()Landroid/app/Application;

    move-result-object p1

    .line 51
    instance-of v0, p1, Lcom/github/droidfu/DroidFuApplication;

    if-eqz v0, :cond_0

    .line 52
    check-cast p1, Lcom/github/droidfu/DroidFuApplication;

    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Class;->getCanonicalName()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0, p0}, Lcom/github/droidfu/DroidFuApplication;->setActiveContext(Ljava/lang/String;Landroid/content/Context;)V

    :cond_0
    return-void
.end method

.method protected onCreateDialog(I)Landroid/app/Dialog;
    .locals 1

    .line 157
    iget p1, p0, Lcom/github/droidfu/activities/BetterMapActivity;->progressDialogTitleId:I

    .line 158
    iget v0, p0, Lcom/github/droidfu/activities/BetterMapActivity;->progressDialogMsgId:I

    .line 157
    invoke-static {p0, p1, v0}, Lcom/github/droidfu/activities/BetterActivityHelper;->createProgressDialog(Landroid/app/Activity;II)Landroid/app/ProgressDialog;

    move-result-object p1

    return-object p1
.end method

.method public onKeyDown(ILandroid/view/KeyEvent;)Z
    .locals 0

    .line 221
    invoke-static {p0, p1}, Lcom/github/droidfu/activities/BetterActivityHelper;->handleApplicationClosing(Landroid/content/Context;I)V

    .line 222
    invoke-super {p0, p1, p2}, Lcom/google/android/maps/MapActivity;->onKeyDown(ILandroid/view/KeyEvent;)Z

    move-result p1

    return p1
.end method

.method public onNewIntent(Landroid/content/Intent;)V
    .locals 0

    .line 83
    invoke-super {p0, p1}, Lcom/google/android/maps/MapActivity;->onNewIntent(Landroid/content/Intent;)V

    .line 84
    iput-object p1, p0, Lcom/github/droidfu/activities/BetterMapActivity;->currentIntent:Landroid/content/Intent;

    return-void
.end method

.method protected onPause()V
    .locals 1

    .line 58
    invoke-super {p0}, Lcom/google/android/maps/MapActivity;->onPause()V

    const/4 v0, 0x0

    .line 59
    iput-boolean v0, p0, Lcom/github/droidfu/activities/BetterMapActivity;->wasInterrupted:Z

    iput-boolean v0, p0, Lcom/github/droidfu/activities/BetterMapActivity;->wasCreated:Z

    .line 61
    iget-object v0, p0, Lcom/github/droidfu/activities/BetterMapActivity;->myLocationOverlay:Lcom/google/android/maps/MyLocationOverlay;

    if-eqz v0, :cond_0

    .line 62
    iget-object v0, p0, Lcom/github/droidfu/activities/BetterMapActivity;->myLocationOverlay:Lcom/google/android/maps/MyLocationOverlay;

    invoke-virtual {v0}, Lcom/google/android/maps/MyLocationOverlay;->disableMyLocation()V

    :cond_0
    return-void
.end method

.method protected onRestoreInstanceState(Landroid/os/Bundle;)V
    .locals 0

    .line 77
    invoke-super {p0, p1}, Lcom/google/android/maps/MapActivity;->onRestoreInstanceState(Landroid/os/Bundle;)V

    const/4 p1, 0x1

    .line 78
    iput-boolean p1, p0, Lcom/github/droidfu/activities/BetterMapActivity;->wasInterrupted:Z

    return-void
.end method

.method protected onResume()V
    .locals 1

    .line 68
    invoke-super {p0}, Lcom/google/android/maps/MapActivity;->onResume()V

    .line 70
    iget-object v0, p0, Lcom/github/droidfu/activities/BetterMapActivity;->myLocationOverlay:Lcom/google/android/maps/MyLocationOverlay;

    if-eqz v0, :cond_0

    .line 71
    iget-object v0, p0, Lcom/github/droidfu/activities/BetterMapActivity;->myLocationOverlay:Lcom/google/android/maps/MyLocationOverlay;

    invoke-virtual {v0}, Lcom/google/android/maps/MyLocationOverlay;->enableMyLocation()Z

    :cond_0
    return-void
.end method

.method protected setMapGestureListener(Lcom/github/droidfu/listeners/MapGestureListener;)V
    .locals 1

    .line 207
    new-instance v0, Landroid/view/GestureDetector;

    invoke-direct {v0, p1}, Landroid/view/GestureDetector;-><init>(Landroid/view/GestureDetector$OnGestureListener;)V

    iput-object v0, p0, Lcom/github/droidfu/activities/BetterMapActivity;->tapDetector:Landroid/view/GestureDetector;

    .line 208
    new-instance p1, Lcom/github/droidfu/activities/BetterMapActivity$3;

    invoke-direct {p1, p0}, Lcom/github/droidfu/activities/BetterMapActivity$3;-><init>(Lcom/github/droidfu/activities/BetterMapActivity;)V

    iput-object p1, p0, Lcom/github/droidfu/activities/BetterMapActivity;->tapListener:Landroid/view/View$OnTouchListener;

    .line 216
    iget-object p1, p0, Lcom/github/droidfu/activities/BetterMapActivity;->mapView:Lcom/google/android/maps/MapView;

    iget-object v0, p0, Lcom/github/droidfu/activities/BetterMapActivity;->tapListener:Landroid/view/View$OnTouchListener;

    invoke-virtual {p1, v0}, Lcom/google/android/maps/MapView;->setOnTouchListener(Landroid/view/View$OnTouchListener;)V

    return-void
.end method

.method public setMapView(I)V
    .locals 0

    .line 170
    invoke-virtual {p0, p1}, Lcom/github/droidfu/activities/BetterMapActivity;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Lcom/google/android/maps/MapView;

    iput-object p1, p0, Lcom/github/droidfu/activities/BetterMapActivity;->mapView:Lcom/google/android/maps/MapView;

    return-void
.end method

.method public setMapViewWithZoom(II)V
    .locals 0

    .line 174
    invoke-virtual {p0, p1}, Lcom/github/droidfu/activities/BetterMapActivity;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Lcom/google/android/maps/MapView;

    iput-object p1, p0, Lcom/github/droidfu/activities/BetterMapActivity;->mapView:Lcom/google/android/maps/MapView;

    .line 176
    invoke-virtual {p0, p2}, Lcom/github/droidfu/activities/BetterMapActivity;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/ZoomControls;

    .line 177
    new-instance p2, Lcom/github/droidfu/activities/BetterMapActivity$1;

    invoke-direct {p2, p0}, Lcom/github/droidfu/activities/BetterMapActivity$1;-><init>(Lcom/github/droidfu/activities/BetterMapActivity;)V

    invoke-virtual {p1, p2}, Landroid/widget/ZoomControls;->setOnZoomInClickListener(Landroid/view/View$OnClickListener;)V

    .line 183
    new-instance p2, Lcom/github/droidfu/activities/BetterMapActivity$2;

    invoke-direct {p2, p0}, Lcom/github/droidfu/activities/BetterMapActivity$2;-><init>(Lcom/github/droidfu/activities/BetterMapActivity;)V

    invoke-virtual {p1, p2}, Landroid/widget/ZoomControls;->setOnZoomOutClickListener(Landroid/view/View$OnClickListener;)V

    return-void
.end method

.method public setMyLocationOverlay(Lcom/google/android/maps/MyLocationOverlay;)V
    .locals 1

    .line 196
    iput-object p1, p0, Lcom/github/droidfu/activities/BetterMapActivity;->myLocationOverlay:Lcom/google/android/maps/MyLocationOverlay;

    .line 197
    iget-object p1, p0, Lcom/github/droidfu/activities/BetterMapActivity;->mapView:Lcom/google/android/maps/MapView;

    invoke-virtual {p1}, Lcom/google/android/maps/MapView;->getOverlays()Ljava/util/List;

    move-result-object p1

    iget-object v0, p0, Lcom/github/droidfu/activities/BetterMapActivity;->myLocationOverlay:Lcom/google/android/maps/MyLocationOverlay;

    invoke-interface {p1, v0}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    return-void
.end method

.method public setProgressDialogMsgId(I)V
    .locals 0

    .line 166
    iput p1, p0, Lcom/github/droidfu/activities/BetterMapActivity;->progressDialogMsgId:I

    return-void
.end method

.method public setProgressDialogTitleId(I)V
    .locals 0

    .line 162
    iput p1, p0, Lcom/github/droidfu/activities/BetterMapActivity;->progressDialogTitleId:I

    return-void
.end method
