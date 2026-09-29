.class public Lcom/helpshift/meta/dto/DeviceDiskSpaceDTO;
.super Ljava/lang/Object;
.source "DeviceDiskSpaceDTO.java"


# instance fields
.field public final phoneFreeSpace:Ljava/lang/String;

.field public final phoneTotalSpace:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 8
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 9
    iput-object p1, p0, Lcom/helpshift/meta/dto/DeviceDiskSpaceDTO;->phoneTotalSpace:Ljava/lang/String;

    .line 10
    iput-object p2, p0, Lcom/helpshift/meta/dto/DeviceDiskSpaceDTO;->phoneFreeSpace:Ljava/lang/String;

    return-void
.end method
