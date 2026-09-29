.class public Lcom/helpshift/controllers/SyncController$DataTypes;
.super Ljava/lang/Object;
.source "SyncController.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/controllers/SyncController;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "DataTypes"
.end annotation


# static fields
.field public static final ANALYTICS_EVENT:Ljava/lang/String; = "data_type_analytics_event"

.field public static final DEVICE:Ljava/lang/String; = "data_type_device"

.field public static final SESSION:Ljava/lang/String; = "data_type_session"

.field public static final SWITCH_USER:Ljava/lang/String; = "data_type_switch_user"

.field public static final USER:Ljava/lang/String; = "data_type_user"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 422
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method
