.class Lcom/crashlytics/android/core/CrashlyticsCore$7$2;
.super Ljava/lang/Object;
.source "CrashlyticsCore.java"

# interfaces
.implements Landroid/content/DialogInterface$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/crashlytics/android/core/CrashlyticsCore$7;->run()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lcom/crashlytics/android/core/CrashlyticsCore$7;


# direct methods
.method constructor <init>(Lcom/crashlytics/android/core/CrashlyticsCore$7;)V
    .locals 0

    .line 980
    iput-object p1, p0, Lcom/crashlytics/android/core/CrashlyticsCore$7$2;->this$1:Lcom/crashlytics/android/core/CrashlyticsCore$7;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/content/DialogInterface;I)V
    .locals 1

    .line 983
    iget-object p2, p0, Lcom/crashlytics/android/core/CrashlyticsCore$7$2;->this$1:Lcom/crashlytics/android/core/CrashlyticsCore$7;

    iget-object p2, p2, Lcom/crashlytics/android/core/CrashlyticsCore$7;->val$latch:Lcom/crashlytics/android/core/CrashlyticsCore$OptInLatch;

    const/4 v0, 0x0

    invoke-virtual {p2, v0}, Lcom/crashlytics/android/core/CrashlyticsCore$OptInLatch;->setOptIn(Z)V

    .line 984
    invoke-interface {p1}, Landroid/content/DialogInterface;->dismiss()V

    return-void
.end method
